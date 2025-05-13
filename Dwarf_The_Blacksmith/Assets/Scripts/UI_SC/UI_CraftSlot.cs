using UnityEngine; // �� ���� �ʿ��մϴ�
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CraftSlot : UI_ItemSlot
{
    private Button itemIconButton;

    protected override void Start()
    {
        base.Start();

        // ItemIcon ������Ʈ�� Button ������Ʈ �߰�
        itemIconButton = transform.Find("ItemIcon").GetComponent<Button>();
        if (itemIconButton != null)
        {
            itemIconButton.onClick.AddListener(OnItemIconClick);
        }

        // ItemIcon ������Ʈ���� Ŭ�� �̺�Ʈ �߰�
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
            // �����Ͱ� ���� ��� ������ �̹����� �����ϰ� ����
            itemImage.sprite = null;
            itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 0); // ������ �����ϰ� ����
            return;
        }

        item.data = _data;

        itemImage.sprite = _data.itemIcon;

        // ������ �̹����� ������ ���, ������ �̹����� �������ϰ� ����
        itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 1); // ������ �������ϰ� ����
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        ui.craftWindow.SetupCraftWindow(item.data as ItemData_Equipment);
    }

    private void OnItemIconClick()
    {
        // ItemIcon�� Ŭ���Ǿ��� �� ������ ���� ����
        ui.craftWindow.SetupCraftWindow(item.data as ItemData_Equipment);
    }
}