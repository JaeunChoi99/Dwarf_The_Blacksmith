using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpDeadState : EnemyState
{
    private Enemy_Imp enemy;
    public ImpDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Imp enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
            enemy.SetZeroVelocity();
    }
}
