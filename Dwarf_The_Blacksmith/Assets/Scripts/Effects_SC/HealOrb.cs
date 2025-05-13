using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOrb : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float healPercent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 플레이어인지 확인합니다.
        PlayerStats playerStats = collision.collider.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            // 플레이어의 체력을 회복합니다.
            int healAmount = Mathf.RoundToInt(playerStats.GetMaxHealthValue() * healPercent);
            playerStats.IncreaseHealthBy(healAmount);

            // 힐 오브를 파괴합니다.
            Destroy(gameObject);
        }
    }
}