using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal on hit effect", menuName = "Data/Item effect/Heal on hit effect")]
public class HealOnHit_Effect : ItemEffect
{
    [Range(0f, 1f)]
    [SerializeField] private float healPercent;

    public GameObject effectPrefab; // 이펙트를 위한 프리팹 변수
    private GameObject instantiatedEffect; // 생성된 이펙트를 저장할 변수

    public override void ExecuteEffect(Transform _enemyPosition)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        int healAmount = Mathf.RoundToInt(playerStats.GetMaxHealthValue() * healPercent);
        Player player = PlayerManager.instance.player;

        bool thirdAttack = player.primaryAttackState.comboCounter == 1;

        // 힐 효과가 발동되는 경우
        if (thirdAttack && player.skill.heal.onCooldown)
        {
            // 이미 생성된 이펙트가 없다면 생성
            if (instantiatedEffect == null && effectPrefab != null)
            {
                instantiatedEffect = Instantiate(effectPrefab, player.transform.position, Quaternion.identity);
            }

            // 힐 효과 적용
            playerStats.IncreaseHealthBy(healAmount);
        }
        // 힐 효과가 발동되지 않는 경우
        else
        {
            // 생성된 이펙트가 있다면 파괴
            if (instantiatedEffect != null)
            {
                Destroy(instantiatedEffect);
                instantiatedEffect = null; // 생성된 이펙트 변수 초기화
            }
        }
    }
}