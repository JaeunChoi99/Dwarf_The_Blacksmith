using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearDeadState : EnemyState
{
    private Enemy_Bear enemy;
    public BearDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Bear _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
