using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SlimeType { big,small}

public class Enemy_Slime : Enemy
{

    [Header("Slime Specific")]
    [SerializeField] private SlimeType slimeType;
    [SerializeField] private int slimesToCreate;
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private Vector2 minCreationVelocity;
    [SerializeField] private Vector2 maxCreationVelocity;

    #region States
    public SlimeIdleState idleState { get; private set; }
    public SlimeMoveState moveState { get; private set; }
    public SlimeBattleState battleState { get; private set; }
    public SlimeAttackState attackState { get; private set; }
    public SlimeStunnedState stunnedState { get; private set; }
    public SlimeDeadState deadState { get; private set; }
    public SlimeDamagedState damagedState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        //슬라임이 반대라서 정방향으로 고쳐줌 Entity
        SetupDefailtFacingDir(-1);

        idleState = new SlimeIdleState(this, stateMachine, "Idle", this);
        moveState = new SlimeMoveState(this, stateMachine, "Move", this);
        battleState = new SlimeBattleState(this, stateMachine, "Battle", this);
        attackState = new SlimeAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SlimeStunnedState(this, stateMachine, "Stunned", this);
        deadState = new SlimeDeadState(this, stateMachine, "Die", this);
        damagedState = new SlimeDamagedState(this, stateMachine, "Damaged", this);
        
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

        //슬라임이니까 분열하려면 없애는게 맞는듯
        StartCoroutine(FadeOutAndDestroy());

        stateMachine.ChangeState(deadState);

        if (slimeType == SlimeType.small)
            return;

        CreateSlimes(slimesToCreate, slimePrefab);


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

    private void CreateSlimes(int  _amountOfSlimes, GameObject _slimePrefab)
    {
        for(int i = 0; i < _amountOfSlimes; i++)
        {
            GameObject newSlime = Instantiate(_slimePrefab, transform.position, Quaternion.identity);

            newSlime.GetComponent<Enemy_Slime>().SetupSlime(facingDir);
        }
    }

    public void SetupSlime(int _facingDir)
    {

        if (_facingDir != facingDir)
            Flip();

        float xVelocity = Random.Range(minCreationVelocity.x, maxCreationVelocity.x);
        float yVelocity = Random.Range(minCreationVelocity.y, maxCreationVelocity.y);

        isKnocked = true;

        GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * -facingDir, yVelocity);

        Invoke("CancelKnockback", 1.5f);
    }

    private void CancelKnockback() => isKnocked = false;
}
