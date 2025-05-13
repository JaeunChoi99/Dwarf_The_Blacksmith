using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    public int comboCounter { get; private set; }

    private float lastTimeAttacked;
    private float comboWindow = 0.2f;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        comboCounter++;
        xInput = 0;
       

        if (comboCounter > 3 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        if (comboCounter >= player.attackMovement.Length)
            comboCounter = 0; // 또는 다른 적절한 로직으로 처리

        player.anim.SetInteger("ComboCounter", comboCounter);

        float attackDir = player.facingDir;

        if(xInput != 0)
            attackDir = xInput;

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        stateTimer = 0.2f;
    }

    public override void Exit()
    {
        base.Exit();

        

       player.StartCoroutine("BusyFor", .05f);

        lastTimeAttacked = Time.time;
        //애니메이션 속도 원상복구
        //player.anim.speed = 1f;
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);

        if (stateTimer <= 0)
            player.SetZeroVelocity();

    }
}
