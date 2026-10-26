using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class FinalJuego : MonoBehaviour
{
    public GameObject panelEpilogo;
    public TMP_Text textoEpilogo;
    public string[] parrafos = new string[4];
    public float esperaInicioEpilogo = 1f;
    public float velocidadTexto = 0.04f;
    public float esperaAntesDeCreditos = 4f;

    private bool finalIniciado = false;
    private bool avanzar = false;
    
    void Start(){
        panelEpilogo.SetActive(false);
    }

    void Update(){
        if (!finalIniciado) return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            avanzar = true;
        }
    }

    

    public void MostrarVictoria(){
        if (finalIniciado) return;
        finalIniciado = true;
        StartCoroutine(SecuenciaFinal());
    }

    IEnumerator SecuenciaFinal(){
        yield return new WaitForSeconds(esperaInicioEpilogo);
        Time.timeScale = 0f;
        panelEpilogo.SetActive(true);
        for (int i = 0; i < parrafos.Length; i++){
            textoEpilogo.text = "";
            string contenido = parrafos[i];
            foreach (char letra in contenido) {
                if (avanzar){
                    textoEpilogo.text = contenido;
                    avanzar = false;
                    break;
                }
                textoEpilogo.text += letra;
                yield return new WaitForSecondsRealtime(
                    velocidadTexto
                );
            }
            
            avanzar = false;
            while (!avanzar)
            {
                yield return null;
            }
            avanzar = false;
        }
        yield return new WaitForSecondsRealtime(
            esperaAntesDeCreditos
        );
        Time.timeScale = 1f;
        SceneManager.LoadScene("Creditos");
    }
}
