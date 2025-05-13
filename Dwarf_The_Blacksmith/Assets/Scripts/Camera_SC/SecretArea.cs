using System.Collections;
using UnityEngine;

public class SecretArea : MonoBehaviour
{
    public float fadeDuration = 1f;
    public bool EnterTheBossRoom = false;


    SpriteRenderer sr;
    Color hiddenColor;
    Coroutine currentCoroutine;

    [SerializeField] GameObject Wall;
    private UI_InGame uiInGame; // UI_InGame 스크립트의 인스턴스를 저장할 변수 추가

    private void Start()
    {
        Wall.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
        hiddenColor = sr.color;

        // UI_InGame 스크립트의 인스턴스를 찾아서 할당합니다.
        uiInGame = FindObjectOfType<UI_InGame>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnterTheBossRoom = true;
            AudioManager.instance.PlayBGM(4);

            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(FadeSprite(true));

            Wall.SetActive(true);

            // UI_InGame의 BosshealthBar를 활성화합니다.
            if (uiInGame != null)
            {
                uiInGame.BosshealthBar.SetActive(true);
            }

            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnterTheBossRoom = false;

            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            // currentCoroutine = StartCoroutine(FadeSprite(false));

            // UI_InGame의 BosshealthBar를 비활성화합니다.
            if (uiInGame != null)
            {
                uiInGame.BosshealthBar.SetActive(false);
            }

        }
    }

    private IEnumerator FadeSprite(bool fadeOut)
    {
        Color startColor = sr.color;
        Color targetColor = fadeOut ? new Color(hiddenColor.r, hiddenColor.g, hiddenColor.b, 0f) : hiddenColor;
        float timeFading = 0f;

        while (timeFading < fadeDuration)
        {
            sr.color = Color.Lerp(startColor, targetColor, timeFading / fadeDuration);
            timeFading += Time.deltaTime;
            yield return null;
        }

        sr.color = targetColor;
    }
}