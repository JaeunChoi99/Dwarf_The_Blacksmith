using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Entity entity => GetComponentInParent<Entity>();
    private CharacterStats myStats => GetComponentInParent<CharacterStats>();
    private RectTransform myTransform => GetComponent<RectTransform>();
    public Slider fastSlider;
    public Slider slowSlider;
    private CanvasGroup canvasGroup;
    private float lerpSpeed = 0.025f;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        fastSlider.maxValue = myStats.GetMaxHealthValue();
        slowSlider.maxValue = myStats.GetMaxHealthValue();
        fastSlider.value = myStats.currentHealth;

        if (myStats.currentHealth <= 0)
        {
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    private void Update()
    {
        // fastSlider�� ���� �� �����Ӹ��� �ٷ� �ݿ��˴ϴ�.
        fastSlider.value = myStats.currentHealth;

        // slowSlider�� ���� õõ�� fastSlider�� ���󰡰� �˴ϴ�.
        slowSlider.value = Mathf.Lerp(slowSlider.value, fastSlider.value, lerpSpeed);
    }

    private void OnEnable()
    {
        entity.onFlipped += FlipUI;
        myStats.onHealthChanged += UpdateHealthUI;
    }

    private void OnDisable()
    {
        if (entity != null)
            entity.onFlipped -= FlipUI;

        if (myStats != null)
            myStats.onHealthChanged -= UpdateHealthUI;
    }

    private void FlipUI()
    {
        myTransform.Rotate(0, 180, 0);
    }

    public IEnumerator FadeOutAndDestroy()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 0.5f;
            yield return null;
        }

        Destroy(gameObject);
    }
}