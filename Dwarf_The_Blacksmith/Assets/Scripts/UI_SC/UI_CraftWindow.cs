using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class UI_CraftWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName; // 아이템 이름을 표시하는 TextMeshProUGUI
    [SerializeField] private TextMeshProUGUI itemDescription; // 아이템 설명을 표시하는 TextMeshProUGUI
    [SerializeField] private Image itemIcon; // 아이템 아이콘을 표시하는 Image
    [SerializeField] private Button craftButton; // 제작 버튼을 나타내는 Button

    [SerializeField] private Image[] materialImage; // 제작 재료 이미지 배열
    [SerializeField] private TextMeshProUGUI[] materialName; // 제작 재료 이름을 표시하는 TextMeshProUGUI 배열

    public PlayableDirector playableDirector; // PlayableDirector 추가
    public TimelineAsset timelineAsset; // Timeline 에셋을 위한 변수

    private void Start()
    {
        // PlayableDirector에 Timeline 에셋 할당
        if (playableDirector != null && timelineAsset != null)
        {
            playableDirector.playableAsset = timelineAsset;
        }
        else
        {
            Debug.LogWarning("PlayableDirector 또는 TimelineAsset이 할당되지 않았습니다.");
        }
    }

    // 제작 창을 설정하는 메서드
    public void SetupCraftWindow(ItemData_Equipment _data)
    {
        // 기존에 할당된 모든 클릭 리스너 제거
        craftButton.onClick.RemoveAllListeners();

        // 모든 제작 재료 이미지 및 이름 숨김
        for (int i = 0; i < materialImage.Length; i++)
        {
            materialImage[i].color = Color.clear;
            materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
            materialName[i].color = Color.clear;
        }

        // 제작 재료 표시
        for (int i = 0; i < _data.craftingMaterials.Count; i++)
        {
            if (_data.craftingMaterials.Count > materialImage.Length)
                Debug.LogWarning("");

            // 제작 재료 이미지, 이름 및 수량 표시
            materialImage[i].sprite = _data.craftingMaterials[i].data.itemIcon;
            materialImage[i].color = Color.white;

            materialName[i].text = _data.craftingMaterials[i].data.itemName;
            materialName[i].color = Color.white;

            TextMeshProUGUI materialSlotText = materialImage[i].GetComponentInChildren<TextMeshProUGUI>();
            materialSlotText.text = _data.craftingMaterials[i].stackSize.ToString();
            materialSlotText.color = Color.white;
        }

        // 아이템 정보 표시
        itemIcon.sprite = _data.itemIcon;
        itemName.text = _data.itemName;
        itemDescription.text = _data.GetDescription();

        craftButton.onClick.AddListener(() =>
        {
            if (Inventory.instance.CanCraft(_data, _data.craftingMaterials))
            {
                // 코루틴 호출
                StartCoroutine(PlayCraftingAnimation());
            }
            else
            {
                Debug.Log("제작 불가!");
            }
        });
    }

    private IEnumerator PlayCraftingAnimation()
    {
        // 게임 시간을 활성화
        Time.timeScale = 1;

        // 타임라인 재생
        if (playableDirector != null)
        {
            playableDirector.Play();
            yield return new WaitForSeconds((float)playableDirector.duration);
        }

        // 게임 시간을 다시 멈춤
        Time.timeScale = 0;
        Debug.Log("제작 성공!");
    }
}