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
        // ���� GameObject�� ������ �ִ� UI_EquipmentSlot ������Ʈ�� ã�Ƽ� �����ɴϴ�.
        equipSlot = GetComponentInChildren<UI_EquipmentSlot>();

        if (equipSlot == null)
        {
            Debug.LogError("UI_EquipmentSlot ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        image = GetComponent<Image>();
    }

    private void Update()
    {
        image.sprite = equipSlot.itemImage.sprite;
    }
}
