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
        // PlayerManager�� �ν��Ͻ��� �̹� �����ϴ��� Ȯ���ϰ�, �������� �ʴ´ٸ� ���� �ν��Ͻ��� �Ҵ��մϴ�.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // �̹� �ٸ� �ν��Ͻ��� �����Ѵٸ�, ���� �ν��Ͻ��� �ı��մϴ�.
            Destroy(gameObject);
            return;
        }

        // ���� �÷��̾� �Ҵ� �ڵ�
        // ���� ���, �÷��̾ �� ������ ã������ �Ҵ�Ǵ� ���:
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}


