using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgeAnimationPlayer : MonoBehaviour
{
    private Button button;

    public Animator targetAnimator; // �ִϸ��̼��� ����� ������Ʈ�� Animator

    void Start()
    {
        button = GetComponent<Button>();

        // ��ư Ŭ�� ������ �߰�
        button.onClick.AddListener(OnButtonClick);
        Debug.Log("��ư �����ʰ� ��ϵǾ����ϴ�.");
    }

    private void OnButtonClick()
    {
        Debug.Log("��ư Ŭ����!"); // ��ư Ŭ�� �� �α�
        if (Inventory.instance.canForge)
        {
            Debug.Log("�ִϸ��̼� ��� ����!"); // �ִϸ��̼� ��� �� �α�

            if (targetAnimator != null)
            {
                targetAnimator.Play(targetAnimator.runtimeAnimatorController.animationClips[0].name); // ù ��° �ִϸ��̼� Ŭ���� ��� ���
            }
            else
            {
                Debug.LogWarning("targetAnimator�� �Ҵ���� �ʾҽ��ϴ�.");
            }
        }
        else
        {
            Debug.Log("������ ���� �������� ����."); // canForge�� false�� �� �α�
        }
    }
}
