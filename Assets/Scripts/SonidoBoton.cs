using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SonidoBoton : MonoBehaviour
{
    public AudioClip sonidoClick;
    private static AudioSource audioSourceGlobal;
    private Button boton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boton = GetComponent<Button>();
        
        if (audioSourceGlobal == null){
            GameObject audioObj = new GameObject("AudioUI_Global");
            audioSourceGlobal = audioObj.AddComponent<AudioSource>();
        }
        boton.onClick.AddListener(ReproducirClick);
    }

    void ReproducirClick(){
        if (sonidoClick != null && audioSourceGlobal != null){
            audioSourceGlobal.PlayOneShot(sonidoClick);
        }
    }
}
