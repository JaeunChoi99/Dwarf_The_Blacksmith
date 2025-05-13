using System.Collections;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float damage = 10f;
    [SerializeField] private string targetLayerName = "Player";
    private Animator anim;
    private Rigidbody2D rb; // Rigidbody2D ���� �߰�
    private bool isGrounded = false;
    private float groundedTimer = 0f;
    private const float groundedDelay = 0.5f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
        anim.SetBool("Idle", true); // ������ �� Idle �ִϸ��̼� ����
        anim.SetBool("Grounded", false); // �ʱ� ���¿��� Grounded �ִϸ��̼� false�� ����
    }

    public void StartFalling()
    {
        StartCoroutine(FallAndDestroy());
    }

    private IEnumerator FallAndDestroy()
    {
        // �������� ����
        while (transform.position.y > -5f) // �ٴ� ��ġ ����
        {
            rb.velocity = Vector2.down * fallSpeed; // Rigidbody2D�� ����Ͽ� �ϰ�
            yield return null;
        }
        DestroyStalactite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(targetLayerName)))
        {
            CharacterStats targetStats = collision.GetComponent<CharacterStats>();
            if (targetStats != null && !targetStats.isInvincible) // ���� ���°� �ƴ� ���� ó��
            {
                if (!isGrounded)
                {
                // �浹 �� ��� ������ ó��
                targetStats.DoDamage(targetStats); // targetStats�� �������� ��
                }
                DestroyStalactite(); // �浹 �� �ı� ó��
            }
        }
        else if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            StopFalling(); // �������� ����
            anim.SetBool("Idle", false);
            anim.SetBool("Grounded", true);
            fallSpeed = 0f;

            // �ٴڿ� ����� ��
            isGrounded = true; // Grounded ���� ����
            groundedTimer = 0f; // Ÿ�̸� �ʱ�ȭ
        }
    }

    private void StopFalling()
    {
        rb.velocity = Vector2.zero; // Rigidbody2D�� �ӵ��� 0���� ����
    }

    private void Update()
    {
        if (isGrounded)
        {
            groundedTimer += Time.deltaTime; // Ÿ�̸� ������Ʈ
            if (groundedTimer >= groundedDelay)
            {
                isGrounded = false; // 0.5�� �� grounded ���� ����
            }
        }
    }

    private void DestroyStalactite()
    {
        StartCoroutine(WaitAndDestroy(5f)); // �ִϸ��̼� ��� �ð� ���
    }

    private IEnumerator WaitAndDestroy(float waitTime)
    {
        anim.SetBool("Grounded", true); // �μ��� �� Grounded �ִϸ��̼� ����
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}