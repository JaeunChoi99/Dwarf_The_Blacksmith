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

        // 항상 플레이어를 추적합니다.
        FollowPlayer();

        // 플레이어 감지 거리 확인 및 공격 상태 전환
        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                {
                    // 랜덤으로 공격 상태를 선택합니다.
                    int attackPattern = Random.Range(0, 2); // 0 또는 1 선택
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
        // 플레이어의 x 위치에 따라 이동 방향 설정
        float distanceToPlayerX = Mathf.Abs(player.position.x - enemy.transform.position.x);

        // 플레이어와의 거리 체크
        if (distanceToPlayerX < .8f)
            return;

        if (player.position.x > enemy.transform.position.x + .3f)
        {
            moveDir = 1; // 오른쪽
        }
        else if (player.position.x < enemy.transform.position.x - .3f)
        {
            moveDir = -1; // 왼쪽
        }

        // 플레이어 방향으로 이동
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
