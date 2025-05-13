using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttackState : EnemyState
{
    private Enemy_Bear enemy;
    public BearAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Bear _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        if (enemy.isDamaged)
            stateMachine.ChangeState(enemy.damagedState);

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}
