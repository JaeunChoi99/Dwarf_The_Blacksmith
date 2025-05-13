using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossHealthBar : MonoBehaviour
{
    private Entity entity;
    private CharacterStats myStats;
    private RectTransform myTransform;
    public Slider fastSlider;
    public Slider slowSlider;
    private CanvasGroup canvasGroup;
    private float lerpSpeed = 0.025f;

    private void Start()
    {
        GameObject bossKobold = GameObject.FindWithTag("BossKobold");

        if (bossKobold == null)
        {
            Debug.LogError("Boss Kobold GameObject is not found with tag 'BossKobold'.");
            return;
        }

        entity = bossKobold.GetComponent<Entity>();
        myStats = bossKobold.GetComponent<CharacterStats>();
        myTransform = GetComponent<RectTransform>();

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (myStats == null)
        {
            Debug.LogError("CharacterStats component is missing on Boss Kobold GameObject.");
            return;
        }

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
        if (myStats != null)
        {
            fastSlider.value = myStats.currentHealth;
            slowSlider.value = Mathf.Lerp(slowSlider.value, fastSlider.value, lerpSpeed);
        }
    }

    private void OnEnable()
    {
        if (entity != null)
            entity.onFlipped -= FlipUI;

        if (myStats != null)
            myStats.onHealthChanged -= UpdateHealthUI;
    }

    private void OnDisable()
    {
        if (entity != null)
            entity.onFlipped += FlipUI;

        if (myStats != null)
            myStats.onHealthChanged += UpdateHealthUI;
    }

    private void FlipUI()
    {
        myTransform.Rotate(0, 180, 0);
    }

    public IEnumerator FadeOutAndDestroy()
    {
        if (canvasGroup != null)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime * 0.5f;
                yield return null;
            }
        }

        Destroy(gameObject);
    }

    // 보스가 사망했을 때 호출되는 함수
    public void BossDied()
    {
        // 체력바를 사라지게 하는 코루틴을 시작합니다.
        StartCoroutine(FadeOutAndDestroy());
    }
}