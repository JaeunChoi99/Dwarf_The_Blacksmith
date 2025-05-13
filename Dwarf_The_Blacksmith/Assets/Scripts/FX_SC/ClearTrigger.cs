using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class ClearTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector; // PlayableDirector
    public TimelineAsset timelineAsset; // Timeline ������ ���� ����
    private bool isPlayed = false;

    private void Start()
    {
        // PlayableDirector�� Timeline ���� �Ҵ�
        if (playableDirector != null && timelineAsset != null)
        {
            playableDirector.playableAsset = timelineAsset;
            playableDirector.stopped += OnTimelineStopped; // Ÿ�Ӷ����� ����Ǿ��� �� ȣ��� �޼��� ���
        }
        else
        {
            Debug.LogWarning("PlayableDirector �Ǵ� TimelineAsset�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayed) // �÷��̾� �±� Ȯ��
        {
            playableDirector.Play(); // Ÿ�Ӷ��� ���
            isPlayed = true;
        }
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        // Ÿ�Ӷ����� ����� �� ���� ������ �̵�
        SceneManager.LoadScene("StartInsertScene"); // "NextSceneName"�� ���� �� �̸����� �����ϼ���.
    }
}