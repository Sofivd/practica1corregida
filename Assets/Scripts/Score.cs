using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    private int monedas;
    public TextMeshProUGUI monedasText;
    void Start()
    {
        monedas = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Recolectable")
        {
            monedas++;
            monedasText.text = "Monedas recolectadas: " + monedas;
            Debug.Log(monedas);
        }
    }
}
