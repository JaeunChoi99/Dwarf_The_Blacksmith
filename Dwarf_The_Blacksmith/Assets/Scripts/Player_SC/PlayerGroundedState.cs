using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.currentJumpCount = player.canJumpCount;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.X))
            stateMachine.ChangeState(player.primaryAttackState);
        
        if (Input.GetKeyDown(KeyCode.V))
            stateMachine.ChangeState(player.counterAttackState);


        if (Input.GetKeyDown(KeyCode.C) && player.IsGroundDetected() || Input.GetKeyDown(KeyCode.C))
            stateMachine.ChangeState(player.jumpState);

    }
}
