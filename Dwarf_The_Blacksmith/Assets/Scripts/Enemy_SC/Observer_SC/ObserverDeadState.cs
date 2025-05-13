using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverDeadState : EnemyState
{
    private Enemy_Observer enemy;
    public ObserverDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Observer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
            enemy.SetZeroVelocity();
    }
}
