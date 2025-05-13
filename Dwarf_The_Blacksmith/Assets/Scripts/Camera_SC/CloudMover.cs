using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float speed = 0.5f; // 구름의 이동 속도
    public float parallaxFactor = 0.5f; // 파럴랙스 효과 계수

    private float initialXPosition; // 초기 X 위치
    private float backgroundOffset;

    void Start()
    {
        // 초기 X 위치 저장
        initialXPosition = transform.position.x;
        backgroundOffset = 0;
    }

    void Update()
    {
        // 구름을 오른쪽으로 이동
        backgroundOffset += speed * Time.deltaTime;
        float parallax = Camera.main.transform.position.x * (1 - parallaxFactor);
        transform.position = new Vector3(initialXPosition + backgroundOffset + parallax, transform.position.y, transform.position.z);

        // 화면 밖으로 나갔는지 확인 (화면 너비를 고려)
        float screenRightEdge = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        float cloudWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        if (transform.position.x - cloudWidth > screenRightEdge)
        {
            // 구름을 왼쪽으로 재배치
            backgroundOffset -= cloudWidth;
        }
    }
}