using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public LayerMask playerLayer; // 플레이어 레이어

    public float knockbackForce = 10f; // 넉백 힘

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 객체가 플레이어인지 확인
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            Rigidbody2D playerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                // 충돌한 플레이어를 넉백시킵니다.
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                playerRigidbody.velocity = knockbackDirection * knockbackForce;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // 충돌한 객체가 플레이어인지 확인
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            Rigidbody2D playerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                // 충돌한 플레이어를 지속적으로 넉백시킵니다.
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                playerRigidbody.velocity = knockbackDirection * knockbackForce;
            }
        }
    }
}
