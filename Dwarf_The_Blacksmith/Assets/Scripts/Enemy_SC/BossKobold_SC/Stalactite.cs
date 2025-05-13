using System.Collections;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float damage = 10f;
    [SerializeField] private string targetLayerName = "Player";
    private Animator anim;
    private Rigidbody2D rb; // Rigidbody2D 변수 추가
    private bool isGrounded = false;
    private float groundedTimer = 0f;
    private const float groundedDelay = 0.5f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        anim.SetBool("Idle", true); // 시작할 때 Idle 애니메이션 설정
        anim.SetBool("Grounded", false); // 초기 상태에서 Grounded 애니메이션 false로 설정
    }

    public void StartFalling()
    {
        StartCoroutine(FallAndDestroy());
    }

    private IEnumerator FallAndDestroy()
    {
        // 떨어지는 동안
        while (transform.position.y > -5f) // 바닥 위치 조정
        {
            rb.velocity = Vector2.down * fallSpeed; // Rigidbody2D를 사용하여 하강
            yield return null;
        }
        DestroyStalactite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(targetLayerName)))
        {
            CharacterStats targetStats = collision.GetComponent<CharacterStats>();
            if (targetStats != null && !targetStats.isInvincible) // 무적 상태가 아닐 때만 처리
            {
                if (!isGrounded)
                {
                // 충돌 시 즉시 데미지 처리
                targetStats.DoDamage(targetStats); // targetStats에 데미지를 줌
                }
                DestroyStalactite(); // 충돌 후 파괴 처리
            }
        }
        else if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            StopFalling(); // 떨어지는 중지
            anim.SetBool("Idle", false);
            anim.SetBool("Grounded", true);
            fallSpeed = 0f;

            // 바닥에 닿았을 때
            isGrounded = true; // Grounded 상태 시작
            groundedTimer = 0f; // 타이머 초기화
        }
    }

    private void StopFalling()
    {
        rb.velocity = Vector2.zero; // Rigidbody2D의 속도를 0으로 설정
    }

    private void Update()
    {
        if (isGrounded)
        {
            groundedTimer += Time.deltaTime; // 타이머 업데이트
            if (groundedTimer >= groundedDelay)
            {
                isGrounded = false; // 0.5초 후 grounded 상태 해제
            }
        }
    }

    private void DestroyStalactite()
    {
        StartCoroutine(WaitAndDestroy(5f)); // 애니메이션 재생 시간 고려
    }

    private IEnumerator WaitAndDestroy(float waitTime)
    {
        anim.SetBool("Grounded", true); // 부서질 때 Grounded 애니메이션 설정
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}