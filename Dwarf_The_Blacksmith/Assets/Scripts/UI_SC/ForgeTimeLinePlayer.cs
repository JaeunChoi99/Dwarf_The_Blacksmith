using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgeAnimationPlayer : MonoBehaviour
{
    private Button button;

    public Animator targetAnimator; // 애니메이션을 재생할 오브젝트의 Animator

    void Start()
    {
        button = GetComponent<Button>();

        // 버튼 클릭 리스너 추가
        button.onClick.AddListener(OnButtonClick);
        Debug.Log("버튼 리스너가 등록되었습니다.");
    }

    private void OnButtonClick()
    {
        Debug.Log("버튼 클릭됨!"); // 버튼 클릭 시 로그
        if (Inventory.instance.canForge)
        {
            Debug.Log("애니메이션 재생 시작!"); // 애니메이션 재생 시 로그

            if (targetAnimator != null)
            {
                targetAnimator.Play(targetAnimator.runtimeAnimatorController.animationClips[0].name); // 첫 번째 애니메이션 클립을 즉시 재생
            }
            else
            {
                Debug.LogWarning("targetAnimator가 할당되지 않았습니다.");
            }
        }
        else
        {
            Debug.Log("아이템 제작 가능하지 않음."); // canForge가 false일 때 로그
        }
    }
}
