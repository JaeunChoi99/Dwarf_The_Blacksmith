using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LoadingAnimation : MonoBehaviour
{
    public GameObject loadingScreen; // �ε� ȭ�� ������Ʈ

    void Start()
    {
        ShowLoadingScreen();
    }

    void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true); // �ε� ȭ�� Ȱ��ȭ
        // ���⼭ �߰� �ε� ������ ����
    }
}
