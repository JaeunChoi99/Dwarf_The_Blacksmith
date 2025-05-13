using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeButton : MonoBehaviour
{
    public static ForgeButton instance; // 오타 수정: instace -> instance

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // canForge 상태에 따라 이펙트를 재생하는 메서드
    public void PlayEffectIfCanForge(bool canForge)
    {
        if (!Inventory.instance.canForge)
        {
            Debug.LogWarning("effectPrefab이 할당되지 않았습니다.");
            
        }
        else
        {
            Debug.Log("아이템 제작 가능하지 않음.");
        }
    }
}