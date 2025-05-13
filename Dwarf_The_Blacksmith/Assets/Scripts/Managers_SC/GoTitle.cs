using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTitle : MonoBehaviour
{
    private void OnEnable()
    {
        LoadingSceneController.LoadScene("MainMenu");
    }
}
