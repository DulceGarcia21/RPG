using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VidasPlayer : MonoBehaviour
{
    public Image vidaPlayer;
    private float anchoVidasPlayer;
    public static int vida;
    private bool haMuerto;
    public GameObject gameover;
    private const int vidasIni = 5;
    

    private float tiempoInmune = 0f;
    private float tiempoEsperaDanio = 1.5f; // Tiempo que pasa parpadeando o sin recibir daño

    void Start()
    {
        anchoVidasPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        haMuerto = false;
        vida = vidasIni;
        gameover.SetActive(false); 
    }

    void Update()
    {
        // Hacemos la cuenta regresiva del tiempo de inmunidad
        if (tiempoInmune > 0)
        {
            tiempoInmune -= Time.deltaTime;
        }
    }

    public void TomarDanio(int danio){
        // Si la vida es mayor a 0 y ya paso el tiempo de inmunidad
        if(vida > 0 && tiempoInmune <= 0){
            tiempoInmune = tiempoEsperaDanio; // Activamos el tiempo de inmunidad
            vida -= danio;
            DibujarVida(vida);
        }
        
        if(vida <= 0 && !haMuerto){
            haMuerto = true;
            StartCoroutine(EjecutarMuerte());
        }
    }

    private void DibujarVida(int vida){
        RectTransform transformaImagen = vidaPlayer.GetComponent<RectTransform>();
        transformaImagen.sizeDelta = new Vector2(anchoVidasPlayer * (float)vida / (float)vidasIni, transformaImagen.sizeDelta.y);
    }

    IEnumerator EjecutarMuerte(){
        yield return new WaitForSeconds(1.2f);
        gameover.SetActive(true);
    }
}
