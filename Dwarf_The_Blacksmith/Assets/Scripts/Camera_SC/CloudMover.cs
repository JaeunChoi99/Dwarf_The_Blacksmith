using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float speed = 0.5f; // ������ �̵� �ӵ�
    public float parallaxFactor = 0.5f; // �ķ����� ȿ�� ���

    private float initialXPosition; // �ʱ� X ��ġ
    private float backgroundOffset;

    void Start()
    {
        // �ʱ� X ��ġ ����
        initialXPosition = transform.position.x;
        backgroundOffset = 0;
    }

    void Update()
    {
        // ������ ���������� �̵�
        backgroundOffset += speed * Time.deltaTime;
        float parallax = Camera.main.transform.position.x * (1 - parallaxFactor);
        transform.position = new Vector3(initialXPosition + backgroundOffset + parallax, transform.position.y, transform.position.z);

        // ȭ�� ������ �������� Ȯ�� (ȭ�� �ʺ� ���)
        float screenRightEdge = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        float cloudWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        if (transform.position.x - cloudWidth > screenRightEdge)
        {
            // ������ �������� ���ġ
            backgroundOffset -= cloudWidth;
        }
    }
}