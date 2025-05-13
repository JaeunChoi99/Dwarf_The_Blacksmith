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
        // 보스 코볼드를 태그 'BossKobold'로 찾습니다.
        GameObject bossKobold = GameObject.FindWithTag("BossKobold");
        if (bossKobold == null)
        {
            Debug.LogError("Boss Kobold GameObject is not found with tag 'BossKobold'.");
            return;
        }

        // 보스 코볼드의 Entity 및 CharacterStats 컴포넌트를 가져옵니다.
        entity = bossKobold.GetComponent<Entity>();
        myStats = bossKobold.GetComponent<CharacterStats>();
        // 보스 코볼드의 Animator 컴포넌트를 가져옵니다.
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 보스의 체력이 0보다 작으면
        if (entity.stats.currentHealth <= 0)
        {
            // 보스 죽음 애니메이션을 실행합니다.
            anim.SetBool("BossDie", true);
            StartCoroutine(ActivateTriggerAfterDelay(3f)); // 3초 후에 트리거 활성화
        }
    }

    private IEnumerator ActivateTriggerAfterDelay(float delay)
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delay);

        // triggerOB를 활성화
        triggerOB.SetActive(true);
    }
}