using System.Collections;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public float duration = 5.0f;
    public GameObject keyInfo; // Ű ������ ǥ���� UI ������Ʈ
    private bool isPlayerInTrigger = false;
    private ItemDrop myDropSystem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            keyInfo.SetActive(true); // �÷��̾ ����� �� Ű ���� ǥ��
            isPlayerInTrigger = true;
            Debug.Log("Ǯ�̴�~~!!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            keyInfo.SetActive(false); // �÷��̾ ������ Ű ���� ����
        }
    }

    private void Start()
    {
        myDropSystem = GetComponent<ItemDrop>();
        keyInfo.SetActive(false); // �ʱ⿡�� Ű ������ ����
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.D)) // 'D' Ű�� ���� �������� ����
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
        Destroy(gameObject); // ������ ������Ʈ�� ����
    }
}