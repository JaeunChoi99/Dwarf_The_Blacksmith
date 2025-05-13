using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeButton : MonoBehaviour
{
    public static ForgeButton instance; // ��Ÿ ����: instace -> instance

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // canForge ���¿� ���� ����Ʈ�� ����ϴ� �޼���
    public void PlayEffectIfCanForge(bool canForge)
    {
        if (!Inventory.instance.canForge)
        {
            Debug.LogWarning("effectPrefab�� �Ҵ���� �ʾҽ��ϴ�.");
            
        }
        else
        {
            Debug.Log("������ ���� �������� ����.");
        }
    }
}