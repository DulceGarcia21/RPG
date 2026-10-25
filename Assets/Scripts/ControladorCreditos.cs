using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControladorCreditos : MonoBehaviour
{
    public RectTransform contenedorTexto;
    public float velocidad = 100f;          
    public float posYInicial = -3200;      
    public float posYFinal = 1800f;     

    void Start()
    {
        if (contenedorTexto != null){
            Vector2 pos = contenedorTexto.anchoredPosition;
            pos.y = posYInicial;
            contenedorTexto.anchoredPosition = pos;
        }
    }

    void Update()
    {
        if (contenedorTexto != null)
        {
            contenedorTexto.anchoredPosition += Vector2.up * velocidad * Time.deltaTime;
            if (contenedorTexto.anchoredPosition.y >= posYFinal)
            {
                SaltarCreditos();
            }
        }
    }

    public void SaltarCreditos()
    {
        SceneManager.LoadScene("Inicio");
    }
}
