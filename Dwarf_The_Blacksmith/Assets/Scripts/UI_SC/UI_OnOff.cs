using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OnOff : MonoBehaviour
{
    [SerializeField] private GameObject playerCurrentUI;
    private bool isOpen;

    private void Start()
    {
        isOpen = false;
    }
    void Update()
    {
        

        if (!isOpen && Input.GetKeyDown(KeyCode.I))
        {
            isOpen = true;
            playerCurrentUI.SetActive(isOpen);
        }
        
        if(isOpen && Input.GetKeyDown(KeyCode.I))
        {
            isOpen = false;
            playerCurrentUI.SetActive(isOpen);
        }
    }
}
