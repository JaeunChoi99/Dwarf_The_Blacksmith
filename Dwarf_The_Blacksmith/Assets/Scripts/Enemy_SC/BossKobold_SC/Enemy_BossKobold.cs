using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BossKobold : Enemy
{

    

    private Enemy_BossKobold enemy;
    private CharacterStats myStats;
    private float enrageHealthThreshold = 0.5f;
    public bool enrageTriggered = false;
    public bool firstDetected = false;


    public GameObject effectPrefab; // 이펙트 프리펩
    [SerializeField] public GameObject stalactitePrefab; // 종유석 프리팹
    [SerializeField] public float spawnHeightOffset = 4.0f; // 머리 위에서 소환할 높이
    public GameObject effectYChecker;
    

    #region States
    public BossKoboldIdleState idleState { get; private set; }
    public BossKoboldMoveState moveState { get; private set; }
    public BossKoboldBattleState battleState { get; private set; }
    public BossKoboldAttackState attackState { get; private set; }
    public BossKoboldAttackState2 attackState2 { get; private set; }
    public BossKoboldDeadState deadState { get; private set; }

    public BossKoboldEnrageMoveState enrageMoveState { get; private set; }
    public BossKoboldEnrageBattleState enrageBattleState { get; private set; }
    public BossKoboldEnrageAttackState enrageAttackState { get; private set; }
    public BossKoboldEnrageAttack2State enrageAttack2State { get; private set; }
    public BossKoboldEnrageState enrageState { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();

        SetupDefailtFacingDir(-1);


        idleState = new BossKoboldIdleState(this, stateMachine, "Idle", this);
        moveState = new BossKoboldMoveState(this, stateMachine, "Move", this);
        battleState = new BossKoboldBattleState(this, stateMachine, "Battle", this);
        attackState = new BossKoboldAttackState(this, stateMachine, "Attack", this);
        attackState2 = new BossKoboldAttackState2(this, stateMachine, "Attack2", this);
        deadState = new BossKoboldDeadState(this, stateMachine, "Die", this);

        enrageMoveState = new BossKoboldEnrageMoveState(this, stateMachine, "EnrageMove", this);
        enrageBattleState = new BossKoboldEnrageBattleState(this, stateMachine, "EnrageBattle", this);
        enrageAttackState = new BossKoboldEnrageAttackState(this, stateMachine, "EnrageAttack", this);
        enrageAttack2State = new BossKoboldEnrageAttack2State(this, stateMachine, "EnrageAttack2", this);
        enrageState = new BossKoboldEnrageState(this, stateMachine, "Enrage", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initiallize(idleState);
        myStats = GetComponent<CharacterStats>();
    }

    protected override void Update()
    {
        base.Update();

        //9월 16일 추가한 코드 (1)

        if (!IsPlayerDetected() && !firstDetected)
        {
            rb.velocity = new Vector2(0f, 0f);
        }
        else if(IsPlayerDetected())
        {
            firstDetected = true;
        }
        /////////////////////////////

        if (!enrageTriggered && myStats.currentHealth <= myStats.maxHealth.GetValue() * enrageHealthThreshold)
        {
            EnrageMode();
            enrageTriggered = true;
        }



    }
    private void EnrageMode()
    {
        Debug.Log("Enrage Mode Activated");
        // Add your enrage mode actions here
        anim.SetBool("Enrage", true);
        
        if(IsPlayerDetected())
            stateMachine.ChangeState(enrageState);
    }

    public override void Die()
    {
        base.Die();
        AudioManager.instance.PlaySFX(15, null);

        DestroyAllStalactites(); // 보스가 죽을 때 모든 종유석 제거

        StartCoroutine(FadeOutAndDestroy());
        stateMachine.ChangeState(deadState);
    }

    private void DestroyAllStalactites()
    {
        GameObject[] stalactites = GameObject.FindGameObjectsWithTag("Stalactite");
        foreach (GameObject stalactite in stalactites)
        {
            Destroy(stalactite);
        }
    }


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
