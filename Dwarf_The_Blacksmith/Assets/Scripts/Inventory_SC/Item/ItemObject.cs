using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ItemData itemData;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void SetUpVisuals()
    {
        if (itemData == null)
        {
            return;
        }

        spriteRenderer.sprite = itemData.itemIcon;
        gameObject.name = "Item object - " + itemData.itemName;
    }

    public void SetupItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;

        SetUpVisuals();
    }

    public void PickUpItem()
    {
        StartCoroutine(PickUpAfterDelay());
    }

    private IEnumerator PickUpAfterDelay()
    {


        yield return new WaitForSeconds(0.5f); // 0.5초 대기

        if (!Inventory.instance.CanAddItem() && itemData.itemType.Equals(ItemType.Equipment))
        {
            rb.velocity = new Vector2(0, 7);
            yield break; // 대기 후 조기 반환
        }

        AudioManager.instance.PlaySFX(2, null);
        EffectManager.instance.PlayEffect("PickUpFX", transform.position);
        Inventory.instance.AddItem(itemData);

        // 페이드 아웃 효과
        float fadeDuration = 0.2f; // 페이드 아웃 시간
        float startAlpha = spriteRenderer.color.a;



        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            Color newColor = spriteRenderer.color;
            newColor.a = Mathf.Lerp(startAlpha, 0, normalizedTime);
            spriteRenderer.color = newColor;
            yield return null;
        }

        // 최종적으로 오브젝트 삭제
        Destroy(gameObject);
    }
}