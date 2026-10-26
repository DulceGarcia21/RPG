using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;



public class VidasPlayer : MonoBehaviour

{
    public Image vidaPlayer;
    private float anchoVidasPlayer;
    public static int vida;
    private bool haMuerto;
    public GameObject gameover;
    private const int vidasIni = 5;

    [SerializeField] private float tiempoInvulnerabilidad = 1f;
    private bool esInvulnerable = false;

    void Start()
    {
        anchoVidasPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        haMuerto = false;
        vida = vidasIni;
        gameover.SetActive(false);
        Time.timeScale = 1f;
    }

    public void TomarDanio(int danio){
        if(vida <= 0 || haMuerto || esInvulnerable){
            return;
        }
        vida -= danio;
        if (vida < 0){
            vida = 0;
        }
        DibujarVida(vida);
        
        if(vida <= 0){
            haMuerto = true;
            StartCoroutine(EjecutarMuerte());
        } else{
            StartCoroutine(Invulnerabilidad());
        }
    }

    private IEnumerator Invulnerabilidad(){
        esInvulnerable = true;
        yield return new WaitForSeconds(tiempoInvulnerabilidad);
        esInvulnerable = false;
    }

    private void DibujarVida(int vida){
        RectTransform transformaImagen = vidaPlayer.GetComponent<RectTransform>();
        transformaImagen.sizeDelta = new Vector2(anchoVidasPlayer * (float)vida / (float)vidasIni, transformaImagen.sizeDelta.y);
    }

    IEnumerator EjecutarMuerte(){
        yield return new WaitForSeconds(1.2f);
        gameover.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ReiniciarJuego()
    {
        Time.timeScale = 1f; // Reactivamos el tiempo antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena actual (resetea vidas, enemigos y posiciones)
    }

}
