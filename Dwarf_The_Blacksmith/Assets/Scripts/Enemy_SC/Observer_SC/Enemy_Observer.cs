using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Observer : Enemy
{

    [Header("ArcherInfo")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int arrowDamage;
    [SerializeField] private float arrowSpeed;

    [SerializeField] public Vector2 jumpVelocity;
    public float jumpCooldown;
    public float safeDistance; // 플레이어와의거리
    [HideInInspector] public float lastTimeJumped;

    [Header("Backcheck")]
    [SerializeField] private Transform groundBehindCheck;
    [SerializeField] private Vector2 groundBehindCheckSize;

    #region States
    public ObserverIdleState idleState { get; private set; }
    public ObserverMoveState moveState { get; private set; }
    public ObserverBattleState battleState { get; private set; }
    public ObserverAttackState attackState { get; private set; }
    public ObserverStunnedState stunnedState { get; private set; }
    public ObserverDeadState deadState { get; private set; }
    public ObserverJumpState jumpState { get; private set; }
    public ObserverDamagedState damagedState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();


        idleState = new ObserverIdleState(this, stateMachine, "Idle", this);
        moveState = new ObserverMoveState(this, stateMachine, "Move", this);
        battleState = new ObserverBattleState(this, stateMachine, "Idle", this);
        attackState = new ObserverAttackState(this, stateMachine, "Attack", this);
        stunnedState = new ObserverStunnedState(this, stateMachine, "Stunned", this);
        deadState = new ObserverDeadState(this, stateMachine, "Die", this);
        jumpState = new ObserverJumpState(this, stateMachine, "Jump", this);
        damagedState = new ObserverDamagedState(this, stateMachine, "Damaged", this);
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

        StartCoroutine(FadeOutAndDestroy());
        stateMachine.ChangeState(deadState);
    }

    public override void AnimationSpecialAttackTrigger()
    {
        GameObject newArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        newArrow.GetComponent<Arrow_Controller>().SetUpArrow(arrowSpeed * facingDir, stats);
    }

    //몬스터 죽을떄 스르륵 효과
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

    public bool GroundBehind() => Physics2D.BoxCast(groundBehindCheck.position, groundBehindCheckSize, 0, Vector2.zero, 0, whatIsGround);
    public bool WallBehind() => Physics2D.Raycast(wallCheck.position, Vector2.right * -facingDir, wallCheckDistance + 2, whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireCube(groundBehindCheck.position, groundBehindCheckSize);
    }
}
