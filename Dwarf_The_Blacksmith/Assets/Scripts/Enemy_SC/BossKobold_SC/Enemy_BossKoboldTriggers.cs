using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BossKoboldTriggers : Enemy_AnimationTriggers
{
    private Enemy_BossKobold enemyBossKobold => GetComponentInParent<Enemy_BossKobold>();

    private Enemy_BossKobold BossKobold;
    [SerializeField] private GameObject attackRangeFX;

    private void Awake()
    {
        BossKobold = GetComponentInParent<Enemy_BossKobold>();
        if (BossKobold == null)
        {
            Debug.LogError("BossKobold is not assigned. Please check the hierarchy.");
        }
    }

    private GameObject currentAttackRangeFX;

    private void Update()
    {
        if (BossKobold.enrageTriggered)
        {
            Destroy(currentAttackRangeFX);
        }
    }

    private void BossAttackRangeEffect()
    {
        Vector2 footPosition = BossKobold.transform.position; // ������ ��ġ�� ������
        footPosition.y -= 0.5f; // �� ��ġ�� �ణ �Ʒ��� ���� (�ʿ信 ���� ����)

        Debug.Log("Attack range effect started");
        BossKobold.attackBoxSize = new Vector2(17.2f, 1.43f);
        currentAttackRangeFX = Instantiate(attackRangeFX,footPosition , Quaternion.identity);
    }

    private void BossAttackRangeEffectEnd()
    {
        if (currentAttackRangeFX != null)
        {
            Debug.Log("�ı�");
            Destroy(currentAttackRangeFX);
            BossKobold.attackBoxSize = new Vector2(0f, 0f);
            currentAttackRangeFX = null; // �޸� ������ ���� ������ null�� �ʱ�ȭ
        }
    }

    private void BossAttackSFX()
    {
        AudioManager.instance.PlaySFX(10, null);
    }
    
    private void BossAttack2SFX()
    {
        AudioManager.instance.PlaySFX(11, null);
    }

    private void BossAttack3SFX() 
    {
        AudioManager.instance.PlaySFX(12, null);
    }

    private void BossAttack3SFXStop() 
    {
        AudioManager.instance.StopSFX(12);
    }
}
