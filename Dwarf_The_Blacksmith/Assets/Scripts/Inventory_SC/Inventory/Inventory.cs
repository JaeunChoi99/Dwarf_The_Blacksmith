using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public bool canForge = false;

    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    public List<ItemData_Equipment> equipmentItems; // 전체 장비 아이템 리스트

    public Dictionary<EquipmentType, ItemData_Equipment> equippedItems; // 현재 장착된 장비

    [Header("Inventory UI")]

    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform statSlotParent;

    private UI_ItemSlot[] inventoryItemSlot;
    private UI_ItemSlot[] stashItemSlot;
    private UI_EquipmentSlot[] equipmentSlot;
    private UI_StatSlot[] statSlot;


    [Header("Items cooldown")]
    private float lastTimeUsedFlask;
    private float lastTimeUsedArmor;
    private float armorCooldown;

    public float flaskCooldown { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        equippedItems = new Dictionary<EquipmentType, ItemData_Equipment>();
    }

    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashItemSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        statSlot = statSlotParent.GetComponentsInChildren<UI_StatSlot>();

    }


    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;
        InventoryItem newItem = new InventoryItem(_item);

        ItemData_Equipment oldEquipment = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType.Equals(newEquipment.equipmentType))
                oldEquipment = item.Key;
        }

        if (oldEquipment != null)
        {
            UnequipItem(oldEquipment);
            AddItem(oldEquipment);
        }

        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifiers();

        //장착한 아이템이 무기인지 확인하고 애니메이션 변경
        if (newEquipment.equipmentType.Equals(EquipmentType.WeaponMainHand))
        {
            PlayerManager.instance.player?.ChangeAttackAnimationSet(newEquipment.animationType.ToString());
        }

        RemoveItem(_item);
        UpdateSlotUI();
    }

    public void UnequipItem(ItemData_Equipment itemToRemove)
    {
        if (equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDictionary.Remove(itemToRemove);
            itemToRemove.RemoveModifiers();

            if (itemToRemove.equipmentType.Equals(EquipmentType.WeaponMainHand))
            {
                PlayerManager.instance.player?.DefaultAttack();
            }
        }
    }

    private void UpdateSlotUI()
    {

        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType.Equals(equipmentSlot[i].slotType))
                    equipmentSlot[i].UpdateSlot(item.Value);
            }
        }

        for (int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].CleanUpSlot();
        }



        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventory[i]);
        }

        for (int i = 0; i < stash.Count; i++)
        {
            stashItemSlot[i].UpdateSlot(stash[i]);
        }

        UpdateStatsUI();
    }

    public void UpdateStatsUI()
    {
        for (int i = 0; i < statSlot.Length; i++) // update info of stats in character UI
        {
            statSlot[i].UpdateStatValueUI();
        }
    }

    public void AddItem(ItemData _item)
    {
        if (_item.itemType == ItemType.Equipment && CanAddItem())
            AddToInventory(_item);
        else if (_item.itemType == ItemType.Material)
            AddToStash(_item);


        UpdateSlotUI();
    }

    private void AddToStash(ItemData _item)
    {
        if (stashDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            stash.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }
    }

    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }
    }

    public bool CanAddItem()
    {
        if (inventory.Count >= inventoryItemSlot.Length)
        {
            Debug.Log("가방에 공간이없어요ㅠ");
            return false;
        }

        return true;
    }

    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else
            {
                value.RemoveStack();
            }
        }



        if (stashDictionary.TryGetValue(_item, out InventoryItem stashValue))
        {
            if (stashValue.stackSize <= 1)
            {
                stash.Remove(stashValue);
                stashDictionary.Remove(_item);
            }
            else
                stashValue.RemoveStack();
        }

        UpdateSlotUI();
    }
    public List<InventoryItem> GetEquipmentList() => equipment;
    public List<InventoryItem> GetStashList() => stash;
    public ItemData_Equipment GetEquipment(EquipmentType _type)
    {
        ItemData_Equipment equipedItem = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == _type)
                equipedItem = item.Key;
        }

        return equipedItem;
    }

    public ItemData_Equipment GetAnimType(AnimationType _animtype)
    {
        ItemData_Equipment equipedAnim = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.animationType == _animtype)
                equipedAnim = item.Key;
        }

        return equipedAnim;
    }
    public ItemData_Equipment GetArmorType(ArmorType _armortype)
    {
        ItemData_Equipment equipedArmor = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.armorType == _armortype)
                equipedArmor = item.Key;
        }

        return equipedArmor;
    }

    public bool CanCraft(ItemData_Equipment _itemToCraft, List<InventoryItem> _requiredMaterials)
    {
        if (_itemToCraft == null)
        {
            Debug.LogError("제작하려는 아이템이 null입니다.");
            return false;
        }

        if (_requiredMaterials == null)
        {
            Debug.LogError("필요한 재료 목록이 null입니다.");
            return false;
        }

        // 재료 검사
        foreach (var requiredItem in _requiredMaterials)
        {
            if (requiredItem == null || requiredItem.data == null)
            {
                Debug.LogError("필요한 재료 목록 중 null이 포함되어 있습니다.");
                return false;
            }

            bool found = false;
            int requiredAmount = requiredItem.stackSize;

            // 인벤토리, 장비, 스태시에서 재료 검색
            if (requiredItem.data != null)
            {
                InventoryItem inventoryItem;
                if (inventoryDictionary.TryGetValue(requiredItem.data, out inventoryItem) && inventoryItem.stackSize >= requiredAmount)
                {
                    found = true;
                }

                ItemData_Equipment equipmentData = requiredItem.data as ItemData_Equipment;
                if (!found && equipmentData != null && equipmentDictionary.TryGetValue(equipmentData, out InventoryItem equipmentItem) && equipmentItem.stackSize >= requiredAmount)
                {
                    found = true;
                }

                if (!found && stashDictionary.TryGetValue(requiredItem.data, out InventoryItem stashItem) && stashItem.stackSize >= requiredAmount)
                {
                    found = true;
                }
            }

            if (!found)
            {
                Debug.Log("필요한 재료가 부족합니다: " + requiredItem.data.itemName);
                AudioManager.instance.PlaySFX(19,null);
                return false;
            }
        }

        // 재료 소비
        foreach (var requiredMaterial in _requiredMaterials)
        {
            if (requiredMaterial.data != null)
            {
                RemoveItem(requiredMaterial.data, requiredMaterial.stackSize);
            }
        }

        // 아이템 제작
        AddItem(_itemToCraft);
        Debug.Log("제작 성공: " + _itemToCraft.itemName);

        

        AudioManager.instance.PlaySFX(7, null);
        return true;
    }

    public void RemoveItem(ItemData _item, int amount)
    {
        // 인벤토리에서 아이템 제거
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem inventoryItem))
        {
            if (inventoryItem.stackSize > amount)
            {
                inventoryItem.stackSize -= amount;
            }
            else
            {
                inventory.Remove(inventoryItem);
                inventoryDictionary.Remove(_item);
            }
        }

        // 장비 인벤토리에서 아이템 제거
        ItemData_Equipment equipmentItemData = _item as ItemData_Equipment;
        if (equipmentItemData != null && equipmentDictionary.TryGetValue(equipmentItemData, out InventoryItem equipmentItem))
        {
            if (equipmentItem.stackSize > amount)
            {
                equipmentItem.stackSize -= amount;
            }
            else
            {
                equipment.Remove(equipmentItem);
                equipmentDictionary.Remove(equipmentItemData);
            }
        }

        // Stash에서 아이템 제거
        if (stashDictionary.TryGetValue(_item, out InventoryItem stashItem))
        {
            if (stashItem.stackSize > amount)
            {
                stashItem.stackSize -= amount;
            }
            else
            {
                stash.Remove(stashItem);
                stashDictionary.Remove(_item);
            }
        }
    }

    public void UseFlask()
    {
        ItemData_Equipment currentFlask = GetEquipment(EquipmentType.Flask);

        if (currentFlask == null)
            return;

        bool canUseFlask = Time.time > lastTimeUsedFlask + flaskCooldown;

        if (canUseFlask)
        {
            AudioManager.instance.PlaySFX(33, null);
            EffectManager.instance.PlayEffect("GGSwordSkillFX", new Vector3(PlayerManager.instance.player.transform.position.x, PlayerManager.instance.player.transform.position.y + .24f,0), PlayerManager.instance.player.transform);
            flaskCooldown = currentFlask.itemCooldown;
            currentFlask.Effect(null);
            lastTimeUsedFlask = Time.time;
        }
        else
            Debug.Log("Flask on cooldown;");
    }

    public bool CanUseArmor()
    {
        ItemData_Equipment currentArmor = GetEquipment(EquipmentType.Armor);

        if (Time.time > lastTimeUsedArmor + armorCooldown)
        {
            armorCooldown = currentArmor.itemCooldown;
            lastTimeUsedArmor = Time.time;
            return true;
        }

        Debug.Log("갑옷쿨도는중~");
        return false;
    }


}
