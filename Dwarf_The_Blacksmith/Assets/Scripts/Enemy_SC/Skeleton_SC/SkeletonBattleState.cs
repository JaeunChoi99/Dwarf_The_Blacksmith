using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private Enemy_Skeleton enemy;
    private int moveDir;

    private bool flippedOnce;
    

    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(enemy.moveState);

        stateTimer = enemy.battleTime;
        
        flippedOnce = false;

        
    }
    public override void Update()
    {
        base.Update();

        enemy.anim.SetFloat("xVelocity", enemy.rb.velocity.x);

        if (enemy.isDamaged)
            stateMachine.ChangeState(enemy.damagedState);

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if(enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if(CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            if(flippedOnce == false)
            {
                flippedOnce = true;
                enemy.Flip();   
            }

            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 5)
                stateMachine.ChangeState(enemy.idleState);
        }


        float distanceToPlayerX = Mathf.Abs(player.position.x - enemy.transform.position.x);

        if (distanceToPlayerX < .5f)
            return;

        if (player.position.x > enemy.transform.position.x + .3f)
        {
            moveDir = 1;
        }
        else if (player.position.x < enemy.transform.position.x - .3f)
        {
            moveDir = -1;
        }

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }



    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if(Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }

}
