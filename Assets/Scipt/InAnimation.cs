using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class InAnimation : MonoBehaviour
{
    private float counter = 0f;
    private float speed = 5f;
    public float Xpos = 0f;
    public float Ypos = 0f;

    void Start()
    {
        transform.position = new Vector2(Xpos, Ypos);
    }

    void Update()
    {
        if (counter <= 10f)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(0,0) , Time.deltaTime*speed);
        }
        else if (counter > 10f && counter < 11f)
        {
            transform.position = new Vector2(0, 0);
        }
        counter += Time.deltaTime * speed;
    }
}
