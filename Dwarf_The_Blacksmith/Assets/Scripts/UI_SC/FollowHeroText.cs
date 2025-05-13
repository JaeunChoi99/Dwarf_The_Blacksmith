using UnityEngine;
using TMPro;

public class FollowHeroText : MonoBehaviour
{
    public GameObject hero; // ��� ������Ʈ
    public TextMeshProUGUI followText; // TMP �ؽ�Ʈ

    private void Update()
    {
        // ����� ��ġ�� ������ �ؽ�Ʈ ��ġ�� ������Ʈ
        Vector3 heroScreenPosition = Camera.main.WorldToScreenPoint(hero.transform.position);
        followText.transform.position = new Vector3(heroScreenPosition.x, heroScreenPosition.y + 200, heroScreenPosition.z); // 50 �ȼ� ���� ��ġ
    }
}