using UnityEngine;

public class BossKoboldEnrageBattleState : EnemyState
{
    private Enemy_BossKobold enemy;
    private Transform player;
    private int moveDir;
    private int lastAttackPattern = -1; // ������ ���� ������ �����ϴ� ����

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

        // �׻� �÷��̾ �����մϴ�.
        FollowPlayer();

        // ���� ���� Ȯ��
        if (enemy.IsPlayerDetected() && enemy.IsPlayerDetected().distance < enemy.attackDistance)
        {
            if (CanAttack())
            {
                int attackPattern;

                // ���� ������ ����
                do
                {
                    attackPattern = Random.Range(0, 10); // 0���� 9������ ������ ����
                } while (attackPattern == 1 && lastAttackPattern == 1); // attackPattern 2�� ���ӵ��� �ʵ���

                if (attackPattern < 6) // 80% Ȯ���� attackState ����
                {
                    stateMachine.ChangeState(enemy.enrageAttackState);
                    lastAttackPattern = 0; // ���� ���� 1�� ���
                }
                else // 20% Ȯ���� attackState2 ����
                {
                    stateMachine.ChangeState(enemy.enrageAttack2State);
                    lastAttackPattern = 1; // ���� ���� 2�� ���
                }
            }
        }

        // ���� ���·� ���ư��� ������ �����մϴ�.
        // ������ �÷��̾ �������� �ʾƵ� �г� ���¸� �����մϴ�.
    }

    private void FollowPlayer()
    {
        // �÷��̾��� x ��ġ�� ���� �̵� ���� ����
        float distanceToPlayerX = Mathf.Abs(player.position.x - enemy.transform.position.x);

        // �÷��̾���� �Ÿ� üũ
        if (distanceToPlayerX < .8f)
            return;

        if (player.position.x > enemy.transform.position.x + .3f)
        {
            moveDir = 1; // ������
        }
        else if (player.position.x < enemy.transform.position.x - .3f)
        {
            moveDir = -1; // ����
        }

        // �÷��̾� �������� �̵�
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