using UnityEngine;
using TMPro;

public class FollowHeroText : MonoBehaviour
{
    public GameObject hero; // 용사 오브젝트
    public TextMeshProUGUI followText; // TMP 텍스트

    private void Update()
    {
        // 용사의 위치를 가져와 텍스트 위치를 업데이트
        Vector3 heroScreenPosition = Camera.main.WorldToScreenPoint(hero.transform.position);
        followText.transform.position = new Vector3(heroScreenPosition.x, heroScreenPosition.y + 200, heroScreenPosition.z); // 50 픽셀 위로 위치
    }
}