using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StorySkip : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadingSceneController.LoadScene("MainScene");
        }
    }
}
