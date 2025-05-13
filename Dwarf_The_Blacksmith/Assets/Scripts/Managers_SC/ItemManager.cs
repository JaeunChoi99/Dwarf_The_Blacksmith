using System.Collections;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public float duration = 5.0f;
    public GameObject keyInfo; // 키 정보를 표시할 UI 오브젝트
    private bool isPlayerInTrigger = false;
    private ItemDrop myDropSystem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            keyInfo.SetActive(true); // 플레이어가 닿았을 때 키 정보 표시
            isPlayerInTrigger = true;
            Debug.Log("풀이다~~!!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            keyInfo.SetActive(false); // 플레이어가 나가면 키 정보 숨김
        }
    }

    private void Start()
    {
        myDropSystem = GetComponent<ItemDrop>();
        keyInfo.SetActive(false); // 초기에는 키 정보를 숨김
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.D)) // 'D' 키를 눌러 아이템을 수집
        {
            myDropSystem.GenerateDrop();
            StartCoroutine(PickUp());
        }
    }

    IEnumerator PickUp()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(duration);
        Destroy(gameObject); // 아이템 오브젝트를 삭제
    }
}