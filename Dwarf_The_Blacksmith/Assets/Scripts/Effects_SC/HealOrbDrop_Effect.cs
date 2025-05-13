using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOrbDrop_Effect : ItemEffect
{
    [Range(0f, 1f)]
    [SerializeField] private float healPercent;

    [SerializeField] private GameObject healOrbPrefab;

    [Range(0, 100)]
    [SerializeField] private int dropChance;

    public override void ExecuteEffect(Transform _enemyPosition)
    {
        Debug.Log("ExecuteEffect called");
        // Ȯ���� ���� �� ���긦 ����մϴ�.
        if (Random.Range(0, 1) < dropChance)
        {
            //��� ȿ�� ��Ÿ��
            if (!Inventory.instance.CanUseArmor())
                return;

            Instantiate(healOrbPrefab, _enemyPosition.position, Quaternion.identity);
            Debug.Log("�� ���긦 �����մϴ�.");
        }
        
    }
}