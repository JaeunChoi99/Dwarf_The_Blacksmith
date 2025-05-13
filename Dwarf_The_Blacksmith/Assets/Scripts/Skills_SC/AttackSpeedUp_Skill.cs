using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSpeedUp_Skill : Skill
{
    private Player player;

    // ���� �ִϸ��̼��� ��� �ӵ� ������
    public float attackSpeedMultiplier = 2f;
    // ���� �ִϸ��̼� ��� �ӵ��� �����Ǵ� �ð�(��)
    public float attackSpeedDuration = 3f;

    private float currentAttackSpeedDuration = 0f;

    // Start() �޼��忡�� player ���� �ʱ�ȭ
    void Start()
    {
        // PlayerManager���� �÷��̾� ��������
        player = PlayerManager.instance.player;

        // �÷��̾ null�̸� �α׸� ����ϰ� �Լ� ����
        if (player == null)
        {
            Debug.LogError("Player is not assigned to PlayerManager.");
            return;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();

        // ���� �ִϸ��̼��� ��� �ӵ��� ������ŵ�ϴ�.
        player.anim.speed *= attackSpeedMultiplier;

        // ���� �ִϸ��̼��� ��� �ӵ��� ������ �ð��� �ʱ�ȭ�մϴ�.
        currentAttackSpeedDuration = attackSpeedDuration;
    }

    protected override void Update()
    {
        base.Update();

        // ���� ���� �ִϸ��̼��� ��� �ӵ��� ���� ���̰�,
        // ������ �ð��� ����ϸ� ��� �ӵ��� ������� �ǵ����ϴ�.
        if (currentAttackSpeedDuration > 0)
        {
            currentAttackSpeedDuration -= Time.deltaTime;
            if (currentAttackSpeedDuration <= 0)
            {
                player.anim.speed /= attackSpeedMultiplier;
            }
        }
    }
}