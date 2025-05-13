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
        // 확률에 따라 힐 오브를 드롭합니다.
        if (Random.Range(0, 1) < dropChance)
        {
            //장비 효과 쿨타임
            if (!Inventory.instance.CanUseArmor())
                return;

            Instantiate(healOrbPrefab, _enemyPosition.position, Quaternion.identity);
            Debug.Log("힐 오브를 생성합니다.");
        }
        
    }
}