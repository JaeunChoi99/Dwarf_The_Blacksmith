using UnityEngine; // 이 줄이 필요합니다
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CraftSlot : UI_ItemSlot
{
    private Button itemIconButton;

    protected override void Start()
    {
        base.Start();

        // ItemIcon 오브젝트에 Button 컴포넌트 추가
        itemIconButton = transform.Find("ItemIcon").GetComponent<Button>();
        if (itemIconButton != null)
        {
            itemIconButton.onClick.AddListener(OnItemIconClick);
        }

        // ItemIcon 오브젝트에도 클릭 이벤트 추가
        EventTrigger trigger = itemIconButton.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnItemIconClick(); });
        trigger.triggers.Add(entry);
    }

    public void SetupCraftSlot(ItemData_Equipment _data)
    {
        if (_data == null)
        {
            // 데이터가 없을 경우 아이템 이미지를 투명하게 설정
            itemImage.sprite = null;
            itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 0); // 완전히 투명하게 설정
            return;
        }

        item.data = _data;

        itemImage.sprite = _data.itemIcon;

        // 아이템 이미지가 설정된 경우, 아이템 이미지를 불투명하게 설정
        itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 1); // 완전히 불투명하게 설정
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        ui.craftWindow.SetupCraftWindow(item.data as ItemData_Equipment);
    }

    private void OnItemIconClick()
    {
        // ItemIcon이 클릭되었을 때 실행할 동작 구현
        ui.craftWindow.SetupCraftWindow(item.data as ItemData_Equipment);
    }
}