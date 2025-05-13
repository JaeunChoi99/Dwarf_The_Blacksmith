using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneManager : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject[] trackGroups; // �� Track Group�� GameObject �迭�� ����

    private void Start()
    {
        // ��� Ʈ�� �׷� ��Ȱ��ȭ
        foreach (var group in trackGroups)
        {
            group.SetActive(false);
        }
    }

    public void PlayGroup(int groupIndex)
    {
        // ���õ� Track Group�� Ȱ��ȭ�ϰ� ���
        for (int i = 0; i < trackGroups.Length; i++)
        {
            trackGroups[i].SetActive(i == groupIndex); // ���õ� �ε����� Ȱ��ȭ
        }

        playableDirector.Play(); // Ÿ�Ӷ��� ���
    }
}
