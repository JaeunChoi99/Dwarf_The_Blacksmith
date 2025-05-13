using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal on hit effect", menuName = "Data/Item effect/Heal on hit effect")]
public class HealOnHit_Effect : ItemEffect
{
    [Range(0f, 1f)]
    [SerializeField] private float healPercent;

    public GameObject effectPrefab; // ����Ʈ�� ���� ������ ����
    private GameObject instantiatedEffect; // ������ ����Ʈ�� ������ ����

    public override void ExecuteEffect(Transform _enemyPosition)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        int healAmount = Mathf.RoundToInt(playerStats.GetMaxHealthValue() * healPercent);
        Player player = PlayerManager.instance.player;

        bool thirdAttack = player.primaryAttackState.comboCounter == 1;

        // �� ȿ���� �ߵ��Ǵ� ���
        if (thirdAttack && player.skill.heal.onCooldown)
        {
            // �̹� ������ ����Ʈ�� ���ٸ� ����
            if (instantiatedEffect == null && effectPrefab != null)
            {
                instantiatedEffect = Instantiate(effectPrefab, player.transform.position, Quaternion.identity);
            }

            // �� ȿ�� ����
            playerStats.IncreaseHealthBy(healAmount);
        }
        // �� ȿ���� �ߵ����� �ʴ� ���
        else
        {
            // ������ ����Ʈ�� �ִٸ� �ı�
            if (instantiatedEffect != null)
            {
                Destroy(instantiatedEffect);
                instantiatedEffect = null; // ������ ����Ʈ ���� �ʱ�ȭ
            }
        }
    }
}