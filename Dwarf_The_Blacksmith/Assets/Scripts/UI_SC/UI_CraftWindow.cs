using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class UI_CraftWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName; // ������ �̸��� ǥ���ϴ� TextMeshProUGUI
    [SerializeField] private TextMeshProUGUI itemDescription; // ������ ������ ǥ���ϴ� TextMeshProUGUI
    [SerializeField] private Image itemIcon; // ������ �������� ǥ���ϴ� Image
    [SerializeField] private Button craftButton; // ���� ��ư�� ��Ÿ���� Button

    [SerializeField] private Image[] materialImage; // ���� ��� �̹��� �迭
    [SerializeField] private TextMeshProUGUI[] materialName; // ���� ��� �̸��� ǥ���ϴ� TextMeshProUGUI �迭

    public PlayableDirector playableDirector; // PlayableDirector �߰�
    public TimelineAsset timelineAsset; // Timeline ������ ���� ����

    private void Start()
    {
        // PlayableDirector�� Timeline ���� �Ҵ�
        if (playableDirector != null && timelineAsset != null)
        {
            playableDirector.playableAsset = timelineAsset;
        }
        else
        {
            Debug.LogWarning("PlayableDirector �Ǵ� TimelineAsset�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

    // ���� â�� �����ϴ� �޼���
    public void SetupCraftWindow(ItemData_Equipment _data)
    {
        // ������ �Ҵ�� ��� Ŭ�� ������ ����
        craftButton.onClick.RemoveAllListeners();

        // ��� ���� ��� �̹��� �� �̸� ����
        for (int i = 0; i < materialImage.Length; i++)
        {
            materialImage[i].color = Color.clear;
            materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
            materialName[i].color = Color.clear;
        }

        // ���� ��� ǥ��
        for (int i = 0; i < _data.craftingMaterials.Count; i++)
        {
            if (_data.craftingMaterials.Count > materialImage.Length)
                Debug.LogWarning("");

            // ���� ��� �̹���, �̸� �� ���� ǥ��
            materialImage[i].sprite = _data.craftingMaterials[i].data.itemIcon;
            materialImage[i].color = Color.white;

            materialName[i].text = _data.craftingMaterials[i].data.itemName;
            materialName[i].color = Color.white;

            TextMeshProUGUI materialSlotText = materialImage[i].GetComponentInChildren<TextMeshProUGUI>();
            materialSlotText.text = _data.craftingMaterials[i].stackSize.ToString();
            materialSlotText.color = Color.white;
        }

        // ������ ���� ǥ��
        itemIcon.sprite = _data.itemIcon;
        itemName.text = _data.itemName;
        itemDescription.text = _data.GetDescription();

        craftButton.onClick.AddListener(() =>
        {
            if (Inventory.instance.CanCraft(_data, _data.craftingMaterials))
            {
                // �ڷ�ƾ ȣ��
                StartCoroutine(PlayCraftingAnimation());
            }
            else
            {
                Debug.Log("���� �Ұ�!");
            }
        });
    }

    private IEnumerator PlayCraftingAnimation()
    {
        // ���� �ð��� Ȱ��ȭ
        Time.timeScale = 1;

        // Ÿ�Ӷ��� ���
        if (playableDirector != null)
        {
            playableDirector.Play();
            yield return new WaitForSeconds((float)playableDirector.duration);
        }

        // ���� �ð��� �ٽ� ����
        Time.timeScale = 0;
        Debug.Log("���� ����!");
    }
}