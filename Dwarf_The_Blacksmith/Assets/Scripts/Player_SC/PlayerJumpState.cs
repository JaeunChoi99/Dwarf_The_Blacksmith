using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(8, null);
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        EffectManager.instance.PlayEffect("LandingFX", new Vector3(player.transform.position.x, player.transform.position.y -.5f, 0f));
    }

    public override void Exit()
    {
        AudioManager.instance.StopSFX(8);
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //for move in air
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);

    }
}
