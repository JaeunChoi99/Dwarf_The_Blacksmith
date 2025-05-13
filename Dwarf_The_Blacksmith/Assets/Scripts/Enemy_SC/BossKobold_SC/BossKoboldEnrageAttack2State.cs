using System.Collections;
using UnityEngine;

public class BossKoboldEnrageAttack2State : EnemyState
{
    private Enemy_BossKobold enemy;
    private float spawnInterval = 0.1f; // 각 종유석 사이의 간격
    private int numberOfStalactites = 3; // 떨어뜨릴 종유석 수

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
            // 종유석이 떨어질 위치 계산
            Vector3 spawnPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + enemy.spawnHeightOffset, playerTransform.position.z);
            Vector3 effectPosition = new Vector3(playerTransform.position.x, enemy.effectYChecker.transform.position.y - .7f, enemy.effectYChecker.transform.position.z);
            GameObject effect = Object.Instantiate(enemy.effectPrefab, effectPosition, Quaternion.identity);

            // 종유석을 소환
            GameObject stalactite = Object.Instantiate(enemy.stalactitePrefab, spawnPosition, Quaternion.identity);

            // 잠시 후 이펙트를 파괴 (1초 대기)
            yield return new WaitForSeconds(1f); // 이펙트가 1초간 유지

            // 이펙트 파괴
            if (effect != null)
            {
                Object.Destroy(effect); // Object.Destroy 사용
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}