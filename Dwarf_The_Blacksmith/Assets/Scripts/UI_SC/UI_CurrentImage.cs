using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CurrentImage : MonoBehaviour
{
    private UI_EquipmentSlot equipSlot;
    private Image image;

    private void Start()
    {
        // 현재 GameObject의 하위에 있는 UI_EquipmentSlot 컴포넌트를 찾아서 가져옵니다.
        equipSlot = GetComponentInChildren<UI_EquipmentSlot>();

        if (equipSlot == null)
        {
            Debug.LogError("UI_EquipmentSlot 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        image = GetComponent<Image>();
    }

    private void Update()
    {
        image.sprite = equipSlot.itemImage.sprite;
    }
}
