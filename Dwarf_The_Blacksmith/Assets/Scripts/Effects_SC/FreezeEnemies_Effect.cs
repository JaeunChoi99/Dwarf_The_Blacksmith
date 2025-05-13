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

        //ü���� 10�� ���ϸ� �ߵ��ǰ�
        if(playerStats.currentHealth > playerStats.GetMaxHealthValue() * .1f)
            return;

        //��� ȿ�� ��Ÿ��
        if (!Inventory.instance.CanUseArmor())
            return;

        //�� �󸮴� ��ų�� ������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 2);

        foreach ( var hit in colliders)
        {
            hit.GetComponent<Enemy>()?.FreezeTimeFor(duration);
        }
    }
}
