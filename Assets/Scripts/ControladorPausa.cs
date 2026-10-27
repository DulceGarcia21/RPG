using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControladorPausa : MonoBehaviour
{
   public GameObject panelPausa;
   private bool estaPausado = false;
   public AudioClip sonidoPausa; 
    private AudioSource audioSource;

    void Start(){
        Time.timeScale = 1;
        panelPausa.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null){
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        // Importante: Permite que el audio suene aunque el Time.timeScale sea 0
        audioSource.ignoreListenerPause = true;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (estaPausado){
                CerrarPausa();
            }else{
                AbrirPausa();
            }
        }
    }

    private void CerrarPausa(){
        if (sonidoPausa != null && audioSource != null){
            audioSource.PlayOneShot(sonidoPausa);
        }
        panelPausa.SetActive(false);
        Time.timeScale = 1;
        estaPausado = false;
    }

    private void AbrirPausa(){
        if (sonidoPausa != null && audioSource != null){
            audioSource.PlayOneShot(sonidoPausa);
        }
        panelPausa.SetActive(true);
        Time.timeScale = 0;
        estaPausado = true;
    }

    public void AlternarPausa(){
        if (estaPausado && panelPausa.activeSelf){
            CerrarPausa();
        }else{
            AbrirPausa();
        }
    }
    public void BotonPausa(){
        AlternarPausa();
    }

    public void IrAMenu(){
        CerrarPausa();
        SceneManager.LoadScene("Inicio");
    }
    public void SalirJuego(){
        StartCoroutine(EsperarYSalir());
    }

    IEnumerator EsperarYSalir(){
        if (sonidoPausa != null && audioSource != null){
            audioSource.PlayOneShot(sonidoPausa);
            // Espera exactamente lo que dura el audio (incluso con el juego pausado)
            yield return new WaitForSecondsRealtime(sonidoPausa.length+ 0.3f);
        }
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}
