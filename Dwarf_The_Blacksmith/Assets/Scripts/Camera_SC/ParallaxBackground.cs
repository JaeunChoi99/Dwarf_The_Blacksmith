using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float xParallaxEffect;
    [SerializeField] private float yParallaxEffect;

    private float xPosition;
    private float yPosition;
    private float xLength;
    private float yLength;
    
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        xLength = GetComponent<SpriteRenderer>().bounds.size.x;
        yLength = GetComponent<SpriteRenderer>().bounds.size.y;
        xPosition = transform.position.x;
        yPosition = transform.position.y;
    }


    void Update()
    {
        float xDistanceMoved = cam.transform.position.x * (1 - xParallaxEffect);
        float yDistanceMoved = cam.transform.position.y * (1 - yParallaxEffect);

        float xDistanceToMove = cam.transform.position.x * xParallaxEffect;
        float yDistanceToMove = cam.transform.position.y * yParallaxEffect;

        transform.position = new Vector3(xPosition + xDistanceToMove, yPosition + yDistanceToMove);

        //¹«ÇÑ ¸Ê ¹è°æ
        if (xDistanceMoved > xPosition + xLength)
            xPosition = xPosition + xLength;
        else if (xDistanceMoved < xPosition - xLength)
            xPosition = xPosition - xLength;
    }
}
