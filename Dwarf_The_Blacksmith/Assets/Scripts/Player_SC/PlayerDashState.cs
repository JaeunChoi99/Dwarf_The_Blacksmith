using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerDashState : PlayerState
{
    

    public PlayerDashState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(6, null);
        

        stateTimer = player.dashDuration;
        player.stats.MakeInvincible(true);

        GameObject dashEffect = EffectManager.instance.PlayEffect("DashFX", new Vector3(player.transform.position.x -.8f * player.facingDir,player.transform.position.y -.7f, 0f));
        
        if (dashEffect != null)
        {
            Vector3 effectScale = dashEffect.transform.localScale;
            effectScale.x *= player.facingDir; // 방향에 따라 스케일 반전
            dashEffect.transform.localScale = effectScale;
        }
        
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(6);
        player.SetVelocity(0f, rb.velocity.y);
        player.stats.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        

        if (stateTimer <= 0)
            stateMachine.ChangeState(player.idleState);

        player.fx.CreateAfterImage();
    }
}
