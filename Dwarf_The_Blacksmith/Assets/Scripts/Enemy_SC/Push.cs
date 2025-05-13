using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public LayerMask playerLayer; // �÷��̾� ���̾�

    public float knockbackForce = 10f; // �˹� ��

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ��ü�� �÷��̾����� Ȯ��
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            Rigidbody2D playerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                // �浹�� �÷��̾ �˹��ŵ�ϴ�.
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                playerRigidbody.velocity = knockbackDirection * knockbackForce;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // �浹�� ��ü�� �÷��̾����� Ȯ��
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            Rigidbody2D playerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                // �浹�� �÷��̾ ���������� �˹��ŵ�ϴ�.
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                playerRigidbody.velocity = knockbackDirection * knockbackForce;
            }
        }
    }
}
