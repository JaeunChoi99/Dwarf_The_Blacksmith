using System.Collections;
using UnityEngine;

public class BossKoboldEnrageAttack2State : EnemyState
{
    private Enemy_BossKobold enemy;
    private float spawnInterval = 0.1f; // �� ������ ������ ����
    private int numberOfStalactites = 3; // ����߸� ������ ��

    public BossKoboldEnrageAttack2State(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BossKobold _enemy)
        : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.StartCoroutine(SpawnStalactites());
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.SetZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }

    private IEnumerator SpawnStalactites()
    {
        Transform playerTransform = PlayerManager.instance.player.transform;

        for (int i = 0; i < numberOfStalactites; i++)
        {
            // �������� ������ ��ġ ���
            Vector3 spawnPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + enemy.spawnHeightOffset, playerTransform.position.z);
            Vector3 effectPosition = new Vector3(playerTransform.position.x, enemy.effectYChecker.transform.position.y - .7f, enemy.effectYChecker.transform.position.z);
            GameObject effect = Object.Instantiate(enemy.effectPrefab, effectPosition, Quaternion.identity);

            // �������� ��ȯ
            GameObject stalactite = Object.Instantiate(enemy.stalactitePrefab, spawnPosition, Quaternion.identity);

            // ��� �� ����Ʈ�� �ı� (1�� ���)
            yield return new WaitForSeconds(1f); // ����Ʈ�� 1�ʰ� ����

            // ����Ʈ �ı�
            if (effect != null)
            {
                Object.Destroy(effect); // Object.Destroy ���
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}