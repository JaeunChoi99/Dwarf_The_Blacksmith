using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpTextFX : MonoBehaviour
{
    private TextMeshPro myText;

    [SerializeField] private float speed;
    [SerializeField] private float desapearanceSpeed;
    [SerializeField] private float colorDesapearanceSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private bool isCritical; // 크리티컬 데미지 여부

    private float textTimer;

    void Start()
    {
        myText = GetComponent<TextMeshPro>();
        textTimer = lifeTime;

        if (isCritical)
        {
            myText.color = Color.red; // 크리티컬 데미지일 경우 색상 변경
            myText.fontSize *= 1.5f; // 크리티컬 데미지일 경우 폰트 크기 증가
        }
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), speed * Time.deltaTime);
        textTimer -= Time.deltaTime;

        if (textTimer < 0)
        {
            float alpha = myText.color.a - colorDesapearanceSpeed * Time.deltaTime;
            myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, alpha);

            if (myText.color.a < 50)
                speed = desapearanceSpeed;

            if (myText.color.a <= 0)
                Destroy(gameObject);
        }
    }
}
