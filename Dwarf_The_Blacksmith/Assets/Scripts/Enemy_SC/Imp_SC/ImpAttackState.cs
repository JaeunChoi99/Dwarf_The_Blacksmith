using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpAttackState : EnemyState
{
    private Enemy_Imp enemy;
    public ImpAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Imp enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
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

        if (enemy.isDamaged)
            stateMachine.ChangeState(enemy.damagedState);

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}
