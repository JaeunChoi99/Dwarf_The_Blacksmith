using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_ButtonSound : MonoBehaviour
{
    private Button button;


    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnClickButton()
    {
        AudioManager.instance.PlaySFX(28, null);
        KeepSelected();
    }

    private void KeepSelected()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(button.gameObject);
        }
        else
        {
            Debug.LogWarning("EventSystem이 씬에 없습니다.");
        }
    }

    public void SuccessCraft()
    {
        // 추가 로직이 필요할 경우 여기에 작성
    }
}