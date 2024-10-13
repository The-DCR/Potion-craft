using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    private bool On;
    public bool stop = false;
    public int active = 0;
    
    private void OnMouseOver()
    {
        if (On == true)
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            active = 1;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            On = true;
        }
        if (Input.GetMouseButtonUp(0) || stop == true)
        {
            On = false;
            transform.localScale = new Vector3(0.09f, 0.09f, 0.09f);
            active = 0;
            stop = false;
        }
    }
}
