using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class MovIsaac : MonoBehaviour
{
    public float movX, movY;
    public float fuerzaSalto = 0.5f;
    public float tiempo = 0;

    Rigidbody2D rb;

    public bool saltar = false;
    public bool enSuelo = false;
    public bool gameOver;
    public bool JuegoOn = true;

    public GameObject ParedConPinchos;
    public GameObject Llave;
    public GameObject FinDelJuego;
    public GameObject SonidoDaño;

    SpriteRenderer sprite;

    private Color ColorBase;

    private bool Pausar = false;

    private float TiempoPausado = 2.0f;
    private float TiempoChoque;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        ColorBase = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
     
        if (Input.GetButtonDown("Jump"))
        {
            saltar = true;
        }
        
        if(Pausar && Time.time - TiempoChoque >= TiempoPausado)
        {
            ResetColor();
            Pausar = false;
        }

        if(!Pausar)
        {
            movX = Input.GetAxis("Horizontal");
            movY = Input.GetAxis("Vertical");
            Vector2 direccion = new Vector2(movX, movY);
            transform.Translate(direccion * Time.deltaTime * 10);

            if (Input.GetButtonDown("Jump"))
            {
                saltar = true;
            }
        }

        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    public void Pausa()
    {
        Time.timeScale = 1.0f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        enSuelo = true;

        if (collision.gameObject.name == "suelo")
        {
            enSuelo = true;
        }

        if (collision.gameObject.CompareTag("Enemigo"))
        {
            sprite.color = Color.red;
            Debug.Log("Game Over");
            gameOver = true;
            Pausar = true;
            TiempoChoque = Time.time;
            SonidoDaño.gameObject.SetActive(true);
        }

        if (collision.gameObject.CompareTag("ParedDePinchos"))
        {
            sprite.color = Color.red;
            Debug.Log("Game Over");
            gameOver = true;
            Pausar = true;
            TiempoChoque = Time.time;
            SonidoDaño.gameObject.SetActive(true);
        }
    }
    void FixedUpdate()
    {
        if (saltar && enSuelo)
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            saltar = false;
            enSuelo = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Destroy(collision.gameObject);

        if(collision.gameObject.CompareTag("Llave"))
        {
            Destroy(GameObject.Find("MiniParedPinchos"));
            Destroy(GameObject.Find("MiniParedPinchos2"));
        }

        if (collision.gameObject.CompareTag("Pildora"))
        {
            Debug.Log("Te haces más pequeño");
            transform.localScale = new Vector2(3.2f, 3.2f);
        }

        if(collision.gameObject.CompareTag("Trofeo"))
        {
            Time.timeScale = 0f;
            FinDelJuego.gameObject.SetActive(true);
        }
    }
    private void ResetColor()
    {
        sprite.color = ColorBase;
    }
    public void FinalDelJuego()
    {
        FinDelJuego.gameObject.SetActive(true);
    }
    public void TeHacesDaño()
    {
        SonidoDaño.gameObject.SetActive(false);
    }
}
