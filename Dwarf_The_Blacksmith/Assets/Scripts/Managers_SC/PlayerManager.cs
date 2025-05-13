using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;

    private void Awake()
    {
        // PlayerManager의 인스턴스가 이미 존재하는지 확인하고, 존재하지 않는다면 현재 인스턴스를 할당합니다.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // 이미 다른 인스턴스가 존재한다면, 현재 인스턴스를 파괴합니다.
            Destroy(gameObject);
            return;
        }

        // 이하 플레이어 할당 코드
        // 예를 들어, 플레이어가 씬 내에서 찾아져서 할당되는 경우:
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}


