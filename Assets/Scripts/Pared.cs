using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pared : MonoBehaviour
{
    bool haciaArriba = false;
    void Update()
    {
        if (haciaArriba == true)
        {
            transform.Translate(8 * Vector2.up * Time.deltaTime);
        }

        else
        {
            transform.Translate(8 * Vector2.down * Time.deltaTime);
        }

        if (transform.position.y <= -3)
        {
            haciaArriba = true;

        }

        else if (transform.position.y >= 4)
        {
            haciaArriba = false;
        }
    
    }
}
