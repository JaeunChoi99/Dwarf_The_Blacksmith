using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDoor : MonoBehaviour
{
    private Entity entity;
    private CharacterStats myStats;
    private Animator anim;
    public GameObject triggerOB;

    // Start is called before the first frame update
    void Start()
    {
        // ���� �ں��带 �±� 'BossKobold'�� ã���ϴ�.
        GameObject bossKobold = GameObject.FindWithTag("BossKobold");
        if (bossKobold == null)
        {
            Debug.LogError("Boss Kobold GameObject is not found with tag 'BossKobold'.");
            return;
        }

        // ���� �ں����� Entity �� CharacterStats ������Ʈ�� �����ɴϴ�.
        entity = bossKobold.GetComponent<Entity>();
        myStats = bossKobold.GetComponent<CharacterStats>();
        // ���� �ں����� Animator ������Ʈ�� �����ɴϴ�.
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ������ ü���� 0���� ������
        if (entity.stats.currentHealth <= 0)
        {
            // ���� ���� �ִϸ��̼��� �����մϴ�.
            anim.SetBool("BossDie", true);
            StartCoroutine(ActivateTriggerAfterDelay(3f)); // 3�� �Ŀ� Ʈ���� Ȱ��ȭ
        }
    }

    private IEnumerator ActivateTriggerAfterDelay(float delay)
    {
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(delay);

        // triggerOB�� Ȱ��ȭ
        triggerOB.SetActive(true);
    }
}