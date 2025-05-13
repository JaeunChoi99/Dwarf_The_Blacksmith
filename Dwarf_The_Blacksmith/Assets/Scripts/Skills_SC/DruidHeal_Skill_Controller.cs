using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DruidHeal_Skill_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private float colorLoosingSpeed;
    private float healZoneTimer;
    private List<Enemy> enemiesInHealZone = new List<Enemy>(); // 힐링 존 안에 있는 적 추적
    private List<PlayerStats> playersInHealZone = new List<PlayerStats>(); // 힐링 존 안에 있는 플레이어 추적

    [SerializeField] private float healInterval = .25f; // 힐링이 얼마나 자주 일어날지 설정합니다.
    private float healTimer = 0f;
    private int healAmount = 25; // 플레이어가 힐링 존에 있을 때 받을 힐량
    private float slowFactor = 0.5f; // 적이 힐링 존에 있을 때 받는 슬로우 정도

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        healZoneTimer -= Time.deltaTime;

        if (healZoneTimer < 0)
        {
            // 힐링 존이 사라지기 전에 힐링 존 안에 있던 적들의 이동 속도를 원래대로 복구합니다.
            foreach (var enemy in enemiesInHealZone)
            {
                if (enemy != null)
                {
                    enemy.moveSpeed *= 2f; // 이동속도를 원래대로 증가
                }
            }

            // 힐링 존 자체를 제거합니다.
            Destroy(gameObject);
        }

        healTimer += Time.deltaTime;
        if (healTimer >= healInterval)
        {
            AttemptHeal();
            healTimer = 0f;
        }
    }

    public void SetupHealZone(Transform _newTransform, float _healZoneDuration)
    {
        transform.position = _newTransform.position;
        healZoneTimer = _healZoneDuration;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                // 힐링 존에 들어간 적을 추적합니다.
                enemiesInHealZone.Add(enemy);

                // 적의 이동속도를 감소시킵니다.
                enemy.moveSpeed *= slowFactor;
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                // 힐링 존에 들어간 플레이어를 추적합니다.
                playersInHealZone.Add(playerStats);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                // 힐링 존을 빠져나간 적을 추적합니다.
                enemiesInHealZone.Remove(enemy);

                // 힐링 존을 떠난 적의 이동속도를 복구합니다.
                enemy.moveSpeed *= 2f; // 이동속도를 원래대로 증가
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                // 힐링 존을 빠져나간 플레이어를 추적합니다.
                playersInHealZone.Remove(playerStats);
            }
        }
    }

    private void AttemptHeal()
    {
        foreach (var player in playersInHealZone)
        {
            player.IncreaseHealthBy(healAmount); // 플레이어의 체력을 회복시킵니다.
        }
    }
}