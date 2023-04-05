using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBG : MonoBehaviour
{
    private Vector3 startPos;
    private float getWidth;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        getWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < startPos.x - getWidth)
        {
            transform.position = startPos;
        }
    }
}
