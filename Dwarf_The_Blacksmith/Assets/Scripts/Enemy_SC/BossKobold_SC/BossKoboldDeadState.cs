using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKoboldDeadState : EnemyState
{
    private Enemy_BossKobold enemy;
    public BossKoboldDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BossKobold _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
