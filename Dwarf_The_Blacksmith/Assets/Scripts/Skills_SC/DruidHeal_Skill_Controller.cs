using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DruidHeal_Skill_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private float colorLoosingSpeed;
    private float healZoneTimer;
    private List<Enemy> enemiesInHealZone = new List<Enemy>(); // ���� �� �ȿ� �ִ� �� ����
    private List<PlayerStats> playersInHealZone = new List<PlayerStats>(); // ���� �� �ȿ� �ִ� �÷��̾� ����

    [SerializeField] private float healInterval = .25f; // ������ �󸶳� ���� �Ͼ�� �����մϴ�.
    private float healTimer = 0f;
    private int healAmount = 25; // �÷��̾ ���� ���� ���� �� ���� ����
    private float slowFactor = 0.5f; // ���� ���� ���� ���� �� �޴� ���ο� ����

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        healZoneTimer -= Time.deltaTime;

        if (healZoneTimer < 0)
        {
            // ���� ���� ������� ���� ���� �� �ȿ� �ִ� ������ �̵� �ӵ��� ������� �����մϴ�.
            foreach (var enemy in enemiesInHealZone)
            {
                if (enemy != null)
                {
                    enemy.moveSpeed *= 2f; // �̵��ӵ��� ������� ����
                }
            }

            // ���� �� ��ü�� �����մϴ�.
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
                // ���� ���� �� ���� �����մϴ�.
                enemiesInHealZone.Add(enemy);

                // ���� �̵��ӵ��� ���ҽ�ŵ�ϴ�.
                enemy.moveSpeed *= slowFactor;
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                // ���� ���� �� �÷��̾ �����մϴ�.
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
                // ���� ���� �������� ���� �����մϴ�.
                enemiesInHealZone.Remove(enemy);

                // ���� ���� ���� ���� �̵��ӵ��� �����մϴ�.
                enemy.moveSpeed *= 2f; // �̵��ӵ��� ������� ����
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                // ���� ���� �������� �÷��̾ �����մϴ�.
                playersInHealZone.Remove(playerStats);
            }
        }
    }

    private void AttemptHeal()
    {
        foreach (var player in playersInHealZone)
        {
            player.IncreaseHealthBy(healAmount); // �÷��̾��� ü���� ȸ����ŵ�ϴ�.
        }
    }
}