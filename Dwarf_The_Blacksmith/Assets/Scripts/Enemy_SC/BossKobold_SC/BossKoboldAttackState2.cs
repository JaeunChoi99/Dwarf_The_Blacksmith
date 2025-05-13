using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKoboldAttackState2 : EnemyState
{
    private Enemy_BossKobold enemy;
    public BossKoboldAttackState2(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BossKobold _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttacked = Time.time;

    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();


        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}
