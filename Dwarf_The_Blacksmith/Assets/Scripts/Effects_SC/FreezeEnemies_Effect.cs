using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Freeze enemies effect", menuName = "Data/Item effect/Freeze enemies effect")]

public class FreezeEnemies_Effect : ItemEffect
{
    [SerializeField] private float duration;
    [SerializeField] private GameObject effectPrefab;
    public override void ExecuteEffect(Transform _transform)
    {

        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        //체력이 10퍼 이하면 발동되게
        if(playerStats.currentHealth > playerStats.GetMaxHealthValue() * .1f)
            return;

        //장비 효과 쿨타임
        if (!Inventory.instance.CanUseArmor())
            return;

        //적 얼리는 스킬꺼 가져옴
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 2);

        foreach ( var hit in colliders)
        {
            hit.GetComponent<Enemy>()?.FreezeTimeFor(duration);
        }
    }
}
