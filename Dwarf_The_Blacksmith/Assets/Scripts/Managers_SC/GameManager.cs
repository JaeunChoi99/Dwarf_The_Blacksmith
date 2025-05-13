using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Stages")]
    public int stageIndex;
    public GameObject[] stages;

    private void Awake()
    {
        AudioManager.instance.PlayBGM(1);
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    public void NextStage()
    {
        if(stageIndex < stages.Length -1) 
        {
            AudioManager.instance.PlayBGM(2);
            stages[stageIndex].gameObject.SetActive(false);
            stageIndex++;
            stages[stageIndex].gameObject.SetActive(true);
            PlayerReposition();
    }
        }

    private void PlayerReposition()
    {
        PlayerManager.instance.player.transform.position = new Vector3(0, 0, -1);
        PlayerManager.instance.player.SetZeroVelocity();
    }


    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void PauseGame(bool _pause)
    {
        if (_pause)
            Time.timeScale = 0;
        else
        {
            Time.timeScale = 1;
        }
    }
}
