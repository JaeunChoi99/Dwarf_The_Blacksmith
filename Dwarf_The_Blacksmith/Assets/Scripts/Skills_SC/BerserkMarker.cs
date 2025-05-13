using UnityEngine;

public class BerserkMarker : MonoBehaviour
{
    private Buff_Effect _effect; // Buff_Effect�� Ÿ�� ����

    // Start is called before the first frame update
    void Start()
    {
        // ǥ�� ������Ʈ�� Buff_Effect ������Ʈ ��������
        _effect = GetComponent<Buff_Effect>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ȿ���� ���� �ð��� 0 ������ ���
        if (_effect.buffDuration <= 0)
        {
            // ǥ�� ������Ʈ ����
            Destroy(gameObject);
        }
    }
}