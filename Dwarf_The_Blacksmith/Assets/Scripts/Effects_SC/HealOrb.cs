using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOrb : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float healPercent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ������Ʈ�� �÷��̾����� Ȯ���մϴ�.
        PlayerStats playerStats = collision.collider.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            // �÷��̾��� ü���� ȸ���մϴ�.
            int healAmount = Mathf.RoundToInt(playerStats.GetMaxHealthValue() * healPercent);
            playerStats.IncreaseHealthBy(healAmount);

            // �� ���긦 �ı��մϴ�.
            Destroy(gameObject);
        }
    }
}