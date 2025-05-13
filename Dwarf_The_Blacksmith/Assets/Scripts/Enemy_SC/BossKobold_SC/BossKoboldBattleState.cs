using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKoboldBattleState : EnemyState
{
    private Enemy_BossKobold enemy;
    private Transform player;
    private int moveDir;

    private bool flippedOnce;
    public BossKoboldBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BossKobold _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        if (enemy.enrageTriggered)
        {
            stateMachine.ChangeState(enemy.enrageBattleState);
        }

        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().isDead && !enemy.enrageTriggered)
            stateMachine.ChangeState(enemy.moveState);
    }
    public override void Update()
    {
        base.Update();

        enemy.anim.SetFloat("xVelocity", enemy.rb.velocity.x);

        // �׻� �÷��̾ �����մϴ�.
        FollowPlayer();

        // �÷��̾� ���� �Ÿ� Ȯ�� �� ���� ���� ��ȯ
        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                {
                    // �������� ���� ���¸� �����մϴ�.
                    int attackPattern = Random.Range(0, 2); // 0 �Ǵ� 1 ����
                    if (attackPattern == 0)
                        stateMachine.ChangeState(enemy.attackState);
                    else
                        stateMachine.ChangeState(enemy.attackState2);
                }
            }
        }
    }

    private void FollowPlayer()
    {
        // �÷��̾��� x ��ġ�� ���� �̵� ���� ����
        float distanceToPlayerX = Mathf.Abs(player.position.x - enemy.transform.position.x);

        // �÷��̾���� �Ÿ� üũ
        if (distanceToPlayerX < .8f)
            return;

        if (player.position.x > enemy.transform.position.x + .3f)
        {
            moveDir = 1; // ������
        }
        else if (player.position.x < enemy.transform.position.x - .3f)
        {
            moveDir = -1; // ����
        }

        // �÷��̾� �������� �̵�
        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }



    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
