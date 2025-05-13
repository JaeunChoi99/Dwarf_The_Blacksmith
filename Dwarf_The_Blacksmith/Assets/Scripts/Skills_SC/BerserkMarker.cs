using UnityEngine;

public class BerserkMarker : MonoBehaviour
{
    private Buff_Effect _effect; // Buff_Effect로 타입 변경

    // Start is called before the first frame update
    void Start()
    {
        // 표식 오브젝트의 Buff_Effect 컴포넌트 가져오기
        _effect = GetComponent<Buff_Effect>();
    }

    // Update is called once per frame
    void Update()
    {
        // 버프 효과의 지속 시간이 0 이하인 경우
        if (_effect.buffDuration <= 0)
        {
            // 표식 오브젝트 제거
            Destroy(gameObject);
        }
    }
}