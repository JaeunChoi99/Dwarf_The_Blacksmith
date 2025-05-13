using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneManager : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject[] trackGroups; // 각 Track Group을 GameObject 배열로 설정

    private void Start()
    {
        // 모든 트랙 그룹 비활성화
        foreach (var group in trackGroups)
        {
            group.SetActive(false);
        }
    }

    public void PlayGroup(int groupIndex)
    {
        // 선택된 Track Group만 활성화하고 재생
        for (int i = 0; i < trackGroups.Length; i++)
        {
            trackGroups[i].SetActive(i == groupIndex); // 선택된 인덱스만 활성화
        }

        playableDirector.Play(); // 타임라인 재생
    }
}
