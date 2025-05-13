using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Imp : Enemy
{
    #region States
    public ImpIdleState idleState { get; private set; }
    public ImpMoveState moveState { get; private set; }
    public ImpBattleState battleState { get; private set; }
    public ImpAttackState attackState { get; private set; }
    public ImpStunnedState stunnedState { get; private set; }
    public ImpDeadState deadState { get; private set; }
    public ImpDamagedState damagedState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();

        SetupDefailtFacingDir(-1);

        idleState = new ImpIdleState(this, stateMachine, "Idle", this);
        moveState = new ImpMoveState(this, stateMachine, "Move", this);
        battleState = new ImpBattleState(this, stateMachine, "Battle", this);
        attackState = new ImpAttackState(this, stateMachine, "Attack", this);
        stunnedState = new ImpStunnedState(this, stateMachine, "Stunned", this);
        deadState = new ImpDeadState(this, stateMachine, "Die", this);
        damagedState = new ImpDamagedState(this, stateMachine, "Damaged", this);
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
