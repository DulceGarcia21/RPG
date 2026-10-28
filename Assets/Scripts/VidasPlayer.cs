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
    public EscudoMagico escudoMagico;

    [SerializeField] private float tiempoInvulnerabilidad = 1f;
    private bool esInvulnerable = false;

    public AudioSource audioSource; 
    public AudioClip clipDanio;      
    public AudioClip clipCurar;      
    public AudioClip sonidoGameOver;

    void Start()
    {
        anchoVidasPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        haMuerto = false;
        vida = vidasIni;
        gameover.SetActive(false);
        Time.timeScale = 1f;
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void TomarDanio(int danio){
        if (escudoMagico != null && escudoMagico.EstaActivo()){
            Debug.Log("¡Ataque bloqueado por el escudo!");
            return;
        }
        if (vida <= 0 || haMuerto || esInvulnerable){
            return;
        }
        vida -= danio;
        if (vida < 0){
            vida = 0;
        }
        DibujarVida(vida);

        if (vida <= 0){
            haMuerto = true;
            StartCoroutine(EjecutarMuerte());
        }else{
            if (audioSource != null && clipDanio != null){
                audioSource.PlayOneShot(clipDanio);
            }
            StartCoroutine(Invulnerabilidad());
        }
    }

    public void CurarVida(int cantidad)
    {
        if (vida <= 0 || haMuerto) return;

        vida += cantidad;
        if (vida > vidasIni)
        {
            vida = vidasIni;
        }
        DibujarVida(vida);
        if (audioSource != null && clipCurar != null){
            audioSource.PlayOneShot(clipCurar);
        }
        Debug.Log("Vida curada: +" + cantidad + " | Vida actual: " + vida);
    }

    private IEnumerator Invulnerabilidad()
    {
        esInvulnerable = true;
        yield return new WaitForSeconds(tiempoInvulnerabilidad);
        esInvulnerable = false;
    }

    private void DibujarVida(int vida)
    {
        RectTransform transformaImagen = vidaPlayer.GetComponent<RectTransform>();
        transformaImagen.sizeDelta = new Vector2(anchoVidasPlayer * (float)vida / (float)vidasIni, transformaImagen.sizeDelta.y);
    }

    IEnumerator EjecutarMuerte()
    {
        if (sonidoGameOver != null && audioSource != null){
            audioSource.PlayOneShot(sonidoGameOver);
        }
        yield return new WaitForSeconds(1.2f);
        gameover.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}