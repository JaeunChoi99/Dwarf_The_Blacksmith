using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    [System.Serializable]
    public class Effect
    {
        public GameObject prefab; // 이펙트의 프리팹
        public float lifetime = 1f; // 이펙트 지속 시간
    }

    [SerializeField] private List<Effect> effects; // 이펙트 목록
    private Dictionary<string, Effect> effectDictionary; // 이펙트 사전

    private void Awake()
    {
        // 이펙트를 사전에 저장
        effectDictionary = new Dictionary<string, Effect>();
        foreach (var effect in effects)
        {
            effectDictionary[effect.prefab.name] = effect;
        }

        // Singleton 패턴
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // 부모 Transform을 추가한 PlayEffect 메서드
    public GameObject PlayEffect(string effectName, Vector3 position, Transform parent = null)
    {
        if (effectDictionary.TryGetValue(effectName, out Effect effect))
        {
            GameObject effectInstance = Instantiate(effect.prefab, position, Quaternion.identity);

            // 부모가 설정된 경우 자식으로 설정
            if (parent != null)
            {
                effectInstance.transform.SetParent(parent);
                effectInstance.transform.localPosition = Vector3.zero; // 부모 기준으로 위치 조정
            }

            StartCoroutine(DestroyEffectAfterDelay(effectInstance, effect.lifetime));
            return effectInstance; // 생성한 이펙트 인스턴스를 반환
        }
        else
        {
            Debug.LogWarning($"Effect '{effectName}' not found!");
            return null; // 이펙트가 없을 경우 null 반환
        }
    }

    private IEnumerator DestroyEffectAfterDelay(GameObject effectInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(effectInstance);
    }
}