using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Entity
{


    //게임매니저
    public GameManager gameManager;
    public SkillManager skill;

    public bool isBusy { get; private set; }

    [Header("Attack Details")]
    public Vector2[] attackMovement;

    public float counterAttackDuration = .2f;

    [Header("Move Info")]
    public float moveSpeed = 8f;
    public float jumpForce;
    public int canJumpCount = 1;
    public int currentJumpCount;
    [HideInInspector] public bool canDoubleJump = false;

    [Header("Dash Info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    public PlayerFX fx { get; private set; }

    public Transform currentTarget;







    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDoubleJumpState doubleJumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        doubleJumpState = new PlayerDoubleJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump" );
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

        deadState = new PlayerDeadState(this, stateMachine, "Die");

    }

    protected override void Start()
    {
       base.Start();

        skill = SkillManager.instance;

        fx = GetComponent<PlayerFX>();

       stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        if (Time.timeScale == 0)
            return;

        base.Update();

        stateMachine.currentState.Update();
        ChangeAttackRange();
        CheckForDashInput();

        //플라스크사용
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Inventory.instance.UseFlask();
        }

        //무기스킬 사용

        if (Input.GetKeyDown(KeyCode.LeftShift) && Inventory.instance.GetAnimType(AnimationType.AttackWithGGSword))
        {
            skill.heal.CanUseSkill();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Inventory.instance.GetAnimType(AnimationType.AttackWithGGSword2))
        {
            skill.druidHeal.CanUseSkill();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Inventory.instance.GetAnimType(AnimationType.AttackWithBBSword))
        {
            GameObject BBSEffect = EffectManager.instance.PlayEffect("BBSword2SkillFX", transform.position, transform); // 플레이어의 자식으로 설정
            skill.attackSpeedUp.CanUseSkill();

            Vector3 effectScale = BBSEffect.transform.localScale;
            effectScale.x *= facingDir; // 방향에 따라 스케일 반전
            BBSEffect.transform.localScale = effectScale;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && Inventory.instance.GetAnimType(AnimationType.AttackWithBBSword2))
        {
            GameObject BBSEffect = EffectManager.instance.PlayEffect("BBSword2SkillFX", transform.position, transform); // 플레이어의 자식으로 설정
            skill.attackSpeedUp.CanUseSkill();

            Vector3 effectScale = BBSEffect.transform.localScale;
            effectScale.x *= facingDir; // 방향에 따라 스케일 반전
            BBSEffect.transform.localScale = effectScale;
            skill.thirdExtraHitSkill.CanUseSkill();
        }

        //갑옷 스킬 발동



    }



    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;


        dashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z) && dashUsageTimer < 0)
        {
            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
            {
                dashDir = facingDir;
            }

            stateMachine.ChangeState(dashState);
        }
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }




    //무기변경 : 장착
    public void ChangeAttackAnimationSet(string animBoolName)
    {
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, animBoolName);
    }

    //무기변경 : 헤제
    public void DefaultAttack()
    {
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
    }

    public void ChangeAttackRange()
    {
        if (Inventory.instance.GetAnimType(AnimationType.AttackWithBSword) || Inventory.instance.GetAnimType(AnimationType.AttackWithGGSword) || Inventory.instance.GetAnimType(AnimationType.AttackWithGGSword2) || Inventory.instance.GetAnimType(AnimationType.AttackWithBBSword) || Inventory.instance.GetAnimType(AnimationType.AttackWithBHammer) || Inventory.instance.GetAnimType(AnimationType.AttackWithIBHammer) 
                || Inventory.instance.GetAnimType(AnimationType.AttackWithIBHammer2) || Inventory.instance.GetAnimType(AnimationType.AttackWithTBHammer) || Inventory.instance.GetAnimType(AnimationType.AttackWithTBHammer2))
        {
            if(!Input.GetKeyDown(KeyCode.V))
                attackCheckRadius = 2f;
        }

        else if (Inventory.instance.GetAnimType(AnimationType.AttackWithBBSword2))
        {
            if (!Input.GetKeyDown(KeyCode.V))
                attackCheckRadius = 2.5f;
        }

        else if (Inventory.instance.GetAnimType(AnimationType.AttackWithBSSword) || Inventory.instance.GetAnimType(AnimationType.AttackWithBSSword2) || Inventory.instance.GetAnimType(AnimationType.AttackWithPSSword) || Inventory.instance.GetAnimType(AnimationType.AttackWithPSSword2))
        {
            if (!Input.GetKeyDown(KeyCode.V))
                attackCheckRadius = 1.6f;
        }

        else
        {
            attackCheckRadius = 1.3f;
        }
    }


}