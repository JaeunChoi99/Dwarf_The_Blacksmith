using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Third extra hit effect", menuName = "Data/Item effect/Third extra hit effect")]
public class ThirdExtraHit_Effect : ItemEffect
{
    public GameObject effectPrefab; // 이펙트를 위한 프리팹 변수
    private GameObject instantiatedEffect; // 생성된 이펙트를 저장할 변수

    public override void ExecuteEffect(Transform _enemyPosition)
    {
        // 적에게 추가 데미지 적용
        EnemyStats enemyStats = _enemyPosition.GetComponent<EnemyStats>();
        if (enemyStats != null)
        {
            enemyStats.TakeDamage(10); // 데미지 10 증가
        }

        // 이미 생성된 이펙트가 없다면 생성
        if (instantiatedEffect == null && effectPrefab != null)
        {
            instantiatedEffect = Instantiate(effectPrefab, _enemyPosition.position, Quaternion.identity);
            instantiatedEffect.transform.SetParent(_enemyPosition); // 적의 자식으로 설정
            instantiatedEffect.transform.localPosition = Vector3.zero; // 적의 위치에 맞게 조정

            // 이펙트의 지속 시간을 설정하고 파괴
            Destroy(instantiatedEffect, .7f); // 1초 후에 이펙트를 파괴
        }
    }

    // 이펙트를 초기화하는 메서드 추가
    public void ResetEffect()
    {
        if (instantiatedEffect != null)
        {
            Destroy(instantiatedEffect);
            instantiatedEffect = null; // 생성된 이펙트 변수 초기화
        }
    }
}