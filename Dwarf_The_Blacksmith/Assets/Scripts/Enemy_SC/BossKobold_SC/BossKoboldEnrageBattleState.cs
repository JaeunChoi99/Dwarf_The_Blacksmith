using UnityEngine;

public class BossKoboldEnrageBattleState : EnemyState
{
    private Enemy_BossKobold enemy;
    private Transform player;
    private int moveDir;
    private int lastAttackPattern = -1; // 마지막 공격 패턴을 저장하는 변수

    private bool flippedOnce;

    public BossKoboldEnrageBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BossKobold _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(enemy.enrageMoveState);
    }

    public override void Update()
    {
        base.Update();
        enemy.anim.SetFloat("xVelocity", enemy.rb.velocity.x);

        // 항상 플레이어를 추적합니다.
        FollowPlayer();

        // 공격 조건 확인
        if (enemy.IsPlayerDetected() && enemy.IsPlayerDetected().distance < enemy.attackDistance)
        {
            if (CanAttack())
            {
                int attackPattern;

                // 공격 패턴을 결정
                do
                {
                    attackPattern = Random.Range(0, 10); // 0부터 9까지의 정수를 선택
                } while (attackPattern == 1 && lastAttackPattern == 1); // attackPattern 2가 연속되지 않도록

                if (attackPattern < 6) // 80% 확률로 attackState 선택
                {
                    stateMachine.ChangeState(enemy.enrageAttackState);
                    lastAttackPattern = 0; // 공격 패턴 1을 기억
                }
                else // 20% 확률로 attackState2 선택
                {
                    stateMachine.ChangeState(enemy.enrageAttack2State);
                    lastAttackPattern = 1; // 공격 패턴 2를 기억
                }
            }
        }

        // 원래 상태로 돌아가는 조건을 제거합니다.
        // 보스는 플레이어가 감지되지 않아도 분노 상태를 유지합니다.
    }

    private void FollowPlayer()
    {
        // 플레이어의 x 위치에 따라 이동 방향 설정
        float distanceToPlayerX = Mathf.Abs(player.position.x - enemy.transform.position.x);

        // 플레이어와의 거리 체크
        if (distanceToPlayerX < .8f)
            return;

        if (player.position.x > enemy.transform.position.x + .3f)
        {
            moveDir = 1; // 오른쪽
        }
        else if (player.position.x < enemy.transform.position.x - .3f)
        {
            moveDir = -1; // 왼쪽
        }

        // 플레이어 방향으로 이동
        enemy.SetVelocity(enemy.moveSpeed * moveDir, enemy.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}