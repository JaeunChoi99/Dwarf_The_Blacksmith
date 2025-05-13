using UnityEngine;

public class Arrow_Controller : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private int damage;
    [SerializeField] private string targetLayerName = "Player";

    [SerializeField] private float xVelocity;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private bool canMove;
    [SerializeField] private bool flipped;

    [SerializeField] private float lifeTime = 5.0f;  // 화살이 존재할 수 있는 최대 시간

    private CharacterStats myStats;
    private int facingDir = 1;

    private void Update()
    {
        if (canMove)
            rb.velocity = new Vector2(xVelocity, rb.velocity.y);

        if (facingDir == 1 && rb.velocity.x < 0)
        {
            facingDir = -1;
            transform.Rotate(0, 180, 0);
        }

        if (facingDir == 1 && rb.velocity.x > 0)
        {
            facingDir = -1;
            transform.Rotate(0, 0, 0);
        }

    }

    public void SetUpArrow(float _speed, CharacterStats _myStats)
    {
        sr = GetComponent<SpriteRenderer>();
        xVelocity = _speed;
        myStats = _myStats;

        // 화살이 생성된 후 lifeTime 초 후에 자동으로 파괴
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(targetLayerName)))
        {
            CharacterStats targetStats = collision.GetComponent<CharacterStats>();
            if (targetStats != null && !targetStats.isInvincible)  // 무적 상태가 아닐 때만 처리
            {
                myStats.DoDamage(targetStats);
                StuckInto(collision);
            }
        }
        else if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            StuckInto(collision);
        }
    }

    private void StuckInto(Collider2D collision)
    {
        GetComponentInChildren<ParticleSystem>().Stop();
        GetComponent<CapsuleCollider2D>().enabled = false;
        canMove = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;
        Destroy(gameObject, Random.Range(0.2f, 0.5f));
    }

    public void FlipArrow()
    {
        if (flipped)
            return;

        xVelocity = xVelocity * -1;
        flipped = true;
        transform.Rotate(0, 180, 0);
        targetLayerName = "Enemy";

    }
}
