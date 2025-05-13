using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    [System.Serializable]
    public class Effect
    {
        public GameObject prefab; // ����Ʈ�� ������
        public float lifetime = 1f; // ����Ʈ ���� �ð�
    }

    [SerializeField] private List<Effect> effects; // ����Ʈ ���
    private Dictionary<string, Effect> effectDictionary; // ����Ʈ ����

    private void Awake()
    {
        // ����Ʈ�� ������ ����
        effectDictionary = new Dictionary<string, Effect>();
        foreach (var effect in effects)
        {
            effectDictionary[effect.prefab.name] = effect;
        }

        // Singleton ����
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

    // �θ� Transform�� �߰��� PlayEffect �޼���
    public GameObject PlayEffect(string effectName, Vector3 position, Transform parent = null)
    {
        if (effectDictionary.TryGetValue(effectName, out Effect effect))
        {
            GameObject effectInstance = Instantiate(effect.prefab, position, Quaternion.identity);

            // �θ� ������ ��� �ڽ����� ����
            if (parent != null)
            {
                effectInstance.transform.SetParent(parent);
                effectInstance.transform.localPosition = Vector3.zero; // �θ� �������� ��ġ ����
            }

            StartCoroutine(DestroyEffectAfterDelay(effectInstance, effect.lifetime));
            return effectInstance; // ������ ����Ʈ �ν��Ͻ��� ��ȯ
        }
        else
        {
            Debug.LogWarning($"Effect '{effectName}' not found!");
            return null; // ����Ʈ�� ���� ��� null ��ȯ
        }
    }

    private IEnumerator DestroyEffectAfterDelay(GameObject effectInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(effectInstance);
    }
}