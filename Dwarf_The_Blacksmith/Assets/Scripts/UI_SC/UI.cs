using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [Header("End Screen")]
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject endImage;
    [SerializeField] private GameObject restartButton;

    [SerializeField] private GameObject playerCurrentUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject equipmentsUI;
    [SerializeField] private GameObject inGameUI;


    public UI_ItemTooltip itemToolTip;
    public UI_CraftWindow craftWindow;

    void Start()
    {
        SwitchTo(inGameUI);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            SceneManager.LoadScene(2);

        }


        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    AudioManager.instance.PlaySFX(5, null);
        //    inventoryUI.SetActive(false);
        //    equipmentsUI.SetActive(false);
        //    playerCurrentUI.SetActive(false);
        //    inGameUI.SetActive(true);
        //}

        if (Input.GetKeyDown(KeyCode.I))
        {
            AudioManager.instance.PlaySFX(4, null);
            SwitchWithKeyTo(inventoryUI);
            if(playerCurrentUI != null)
                playerCurrentUI.SetActive(inventoryUI.activeSelf);
        }


        if (Input.GetKeyDown(KeyCode.O))
        {
            AudioManager.instance.PlaySFX(4, null);
            SwitchWithKeyTo(equipmentsUI);
            if(playerCurrentUI != null)
                playerCurrentUI.SetActive(equipmentsUI.activeSelf);
        }
    }

    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            bool fadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null;

            if(fadeScreen == false)
                transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_menu != null)
        {
            AudioManager.instance.PlaySFX(3, null);
            _menu.SetActive(true);
            if (playerCurrentUI != null)
                playerCurrentUI.SetActive(equipmentsUI.activeSelf || inventoryUI.activeSelf);
        }

        if(GameManager.instance != null)
        {
            if (_menu == inGameUI)
                GameManager.instance.PauseGame(false);
            else
                GameManager.instance.PauseGame(true);
        }
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            
            _menu.SetActive(false);
            if (playerCurrentUI != null)
            {
                if (_menu == inventoryUI)
                    playerCurrentUI.SetActive(equipmentsUI.activeSelf);
                else if (_menu == equipmentsUI)
                    playerCurrentUI.SetActive(inventoryUI.activeSelf);
            }
            CheckForInGameUI();
            return;
        }
        SwitchTo(_menu);
    }

    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<UI_FadeScreen>() == null)
                return;
        }

        SwitchTo(inGameUI);
    }

    public void SwitchOnEndScreen()
    {
        fadeScreen.FadeOut();
        StartCoroutine(EndScreenCoroutine());
    }

    IEnumerator EndScreenCoroutine()
    {
        yield return new WaitForSeconds(1);
        endText.SetActive(true);
        endImage.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        restartButton.SetActive(true);
    }

    public void RestartGameButton() => GameManager.instance.RestartScene();
}
