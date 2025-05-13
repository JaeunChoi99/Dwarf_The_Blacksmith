using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemText;

    protected UI ui;
    public InventoryItem item;

    protected virtual void Start()
    {
        ui = GetComponentInParent<UI>();
    }

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;

        itemImage.color = Color.white;

        if (item != null)
        {
            itemImage.sprite = item.data.itemIcon;

            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    public void CleanUpSlot()
    {
        item = null;

        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item == null)
            return;

        AudioManager.instance.PlaySFX(28, null);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Inventory.instance.RemoveItem(item.data);
            return;
        }

        // ItemType�� Equipment�� ��쿡�� EquipItem�� ȣ��
        if (item.data.itemType.Equals(ItemType.Equipment))
        {
            ItemData_Equipment equipmentData = item.data as ItemData_Equipment;
            if (equipmentData != null)
            {
                Inventory.instance.EquipItem(equipmentData);
            }
        }

        ui.itemToolTip.HideToolTip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null)
        {
            return; // �������� ������ �Լ� ����
        }

        Vector2 mousePosition = Input.mousePosition;

        float xOffset = 0;
        float yOffset = 0;

        if (mousePosition.x > 600)
        {
            xOffset = -200; // ���콺 �����Ͱ� �����ʿ� ���� �� x �������� -200���� ����
        }
        else
        {
            xOffset = 200; // ���콺 �����Ͱ� ���ʿ� ���� �� x �������� 200���� ����
        }

        if (mousePosition.y > 320)
        {
            yOffset = -200; // ���콺 �����Ͱ� ���ʿ� ���� �� y �������� -200���� ����
        }
        else
        {
            yOffset = 200; // ���콺 �����Ͱ� �Ʒ��ʿ� ���� �� y �������� 200���� ����
        }

        ui.itemToolTip.ShowToolTip(item.data as ItemData_Equipment); // ������ ������ ������
        ui.itemToolTip.transform.position = new Vector2(mousePosition.x - xOffset, mousePosition.y + yOffset); // ���� ��ġ�� ���콺 ��ġ���� x �������� �� ������ ����
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null)
        {
            return;
        }

        ui.itemToolTip.HideToolTip();
    }
}
