using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxEffect;  // 相当于移动速度(视差效应)
    private float xPosition;
    private float length;
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        // 获取初始位置
        xPosition = transform.position.x;
        
        // 获取背景长度
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {   
        // 计算已经移动的距离
        float movedDistance = cam.transform.position.x * (1 - parallaxEffect);
        float moveDistance = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPosition + moveDistance, transform.position.y);

        if (movedDistance > xPosition + length)
            xPosition += length;
        else if (movedDistance < xPosition - length)
            xPosition -= length;
    }
}
