using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarAnimation : MonoBehaviour
{
    private float counter = 0f;
    private float speed = 5f;
    public float YUp = 0f;
    private float currentY;

    void Start()
    {
        currentY = transform.position.y;
        transform.position = new Vector2(transform.position.x, transform.position.y + YUp);
        transform.localScale = new Vector2(0.0f, 0.0f);
    }

    void Update()
    {
        if (counter <= 10f)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, currentY), Time.deltaTime * speed);
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0.09f, 0.09f), Time.deltaTime * speed);
        }
        else if (counter > 10f && counter < 11f)
        {
            transform.position = new Vector2(transform.position.x, currentY);
            transform.localScale = new Vector2(0.09f, 0.09f);
        }
        counter += Time.deltaTime * speed;
    }
}
