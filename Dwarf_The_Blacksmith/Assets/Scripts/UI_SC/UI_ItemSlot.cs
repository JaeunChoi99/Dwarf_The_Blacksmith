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

        // ItemType이 Equipment인 경우에만 EquipItem을 호출
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
            return; // 아이템이 없으면 함수 종료
        }

        Vector2 mousePosition = Input.mousePosition;

        float xOffset = 0;
        float yOffset = 0;

        if (mousePosition.x > 600)
        {
            xOffset = -200; // 마우스 포인터가 오른쪽에 있을 때 x 오프셋을 -200으로 설정
        }
        else
        {
            xOffset = 200; // 마우스 포인터가 왼쪽에 있을 때 x 오프셋을 200으로 설정
        }

        if (mousePosition.y > 320)
        {
            yOffset = -200; // 마우스 포인터가 위쪽에 있을 때 y 오프셋을 -200으로 설정
        }
        else
        {
            yOffset = 200; // 마우스 포인터가 아래쪽에 있을 때 y 오프셋을 200으로 설정
        }

        ui.itemToolTip.ShowToolTip(item.data as ItemData_Equipment); // 아이템 툴팁을 보여줌
        ui.itemToolTip.transform.position = new Vector2(mousePosition.x - xOffset, mousePosition.y + yOffset); // 툴팁 위치를 마우스 위치에서 x 오프셋을 뺀 곳으로 설정
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
