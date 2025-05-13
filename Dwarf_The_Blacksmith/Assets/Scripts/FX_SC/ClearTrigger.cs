using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class ClearTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector; // PlayableDirector
    public TimelineAsset timelineAsset; // Timeline 에셋을 위한 변수
    private bool isPlayed = false;

    private void Start()
    {
        // PlayableDirector에 Timeline 에셋 할당
        if (playableDirector != null && timelineAsset != null)
        {
            playableDirector.playableAsset = timelineAsset;
            playableDirector.stopped += OnTimelineStopped; // 타임라인이 종료되었을 때 호출될 메서드 등록
        }
        else
        {
            Debug.LogWarning("PlayableDirector 또는 TimelineAsset이 할당되지 않았습니다.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayed) // 플레이어 태그 확인
        {
            playableDirector.Play(); // 타임라인 재생
            isPlayed = true;
        }
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        // 타임라인이 종료된 후 다음 씬으로 이동
        SceneManager.LoadScene("StartInsertScene"); // "NextSceneName"을 실제 씬 이름으로 변경하세요.
    }
}