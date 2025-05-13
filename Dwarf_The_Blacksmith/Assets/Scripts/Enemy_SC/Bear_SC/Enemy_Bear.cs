using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bear : Enemy
{
    #region States
    public BearIdleState idleState { get; private set; }
    public BearMoveState moveState { get; private set; }
    public BearBattleState battleState { get; private set; }
    public BearAttackState attackState { get; private set; }
    public BearStunnedState stunnedState { get; private set; }
    public BearDeadState deadState { get; private set; }
    public BearDamagedState damagedState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        //곰이 반대라서 정방향으로 고쳐줌 Entity
        SetupDefailtFacingDir(-1);

        idleState = new BearIdleState(this, stateMachine, "Idle", this);
        moveState = new BearMoveState(this, stateMachine, "Move", this);
        battleState = new BearBattleState(this, stateMachine, "Battle", this);
        attackState = new BearAttackState(this, stateMachine, "Attack", this);
        stunnedState = new BearStunnedState(this, stateMachine, "Stunned", this);
        deadState = new BearDeadState(this, stateMachine, "Die", this);
        damagedState = new BearDamagedState(this, stateMachine, "Damaged", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initiallize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();

        if (Inventory.instance.GetArmorType(ArmorType.Nature))
        {
            DropHealOrb();
        }

        StartCoroutine(FadeOutAndDestroy());
        stateMachine.ChangeState(deadState);
    }

    //테스트용: 몬스터 죽을떄
    private IEnumerator FadeOutAndDestroy()
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        Color originalColor = sr.color;

        while (sr.color.a > 0)
        {
            // Reduce the alpha value over time
            float newAlpha = sr.color.a - (Time.deltaTime * 1.5f);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            isKnocked = false;

            yield return null;
        }

        // Destroy the game object after fading out
        Destroy(gameObject);
    }
}
