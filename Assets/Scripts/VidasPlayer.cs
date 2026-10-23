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
    public static int puedePerderVida = 1;

    void Start()
    {
        anchoVidasPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        haMuerto = false;
        vida = vidasIni;
        puedePerderVida = 1;
        gameover.SetActive(false);
    }

    public void TomarDanio(int danio){
        if(vida > 0 && puedePerderVida == 1){
            puedePerderVida = 0;
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
