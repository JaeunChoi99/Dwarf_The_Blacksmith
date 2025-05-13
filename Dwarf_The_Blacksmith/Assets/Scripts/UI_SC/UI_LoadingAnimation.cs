using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LoadingAnimation : MonoBehaviour
{
    public GameObject loadingScreen; // 로딩 화면 오브젝트

    void Start()
    {
        ShowLoadingScreen();
    }

    void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true); // 로딩 화면 활성화
        // 여기서 추가 로딩 로직을 구현
    }
}
