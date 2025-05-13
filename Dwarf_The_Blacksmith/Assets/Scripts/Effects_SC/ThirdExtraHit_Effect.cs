using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Third extra hit effect", menuName = "Data/Item effect/Third extra hit effect")]
public class ThirdExtraHit_Effect : ItemEffect
{
    public GameObject effectPrefab; // ����Ʈ�� ���� ������ ����
    private GameObject instantiatedEffect; // ������ ����Ʈ�� ������ ����

    public override void ExecuteEffect(Transform _enemyPosition)
    {
        // ������ �߰� ������ ����
        EnemyStats enemyStats = _enemyPosition.GetComponent<EnemyStats>();
        if (enemyStats != null)
        {
            enemyStats.TakeDamage(10); // ������ 10 ����
        }

        // �̹� ������ ����Ʈ�� ���ٸ� ����
        if (instantiatedEffect == null && effectPrefab != null)
        {
            instantiatedEffect = Instantiate(effectPrefab, _enemyPosition.position, Quaternion.identity);
            instantiatedEffect.transform.SetParent(_enemyPosition); // ���� �ڽ����� ����
            instantiatedEffect.transform.localPosition = Vector3.zero; // ���� ��ġ�� �°� ����

            // ����Ʈ�� ���� �ð��� �����ϰ� �ı�
            Destroy(instantiatedEffect, .7f); // 1�� �Ŀ� ����Ʈ�� �ı�
        }
    }

    // ����Ʈ�� �ʱ�ȭ�ϴ� �޼��� �߰�
    public void ResetEffect()
    {
        if (instantiatedEffect != null)
        {
            Destroy(instantiatedEffect);
            instantiatedEffect = null; // ������ ����Ʈ ���� �ʱ�ȭ
        }
    }
}