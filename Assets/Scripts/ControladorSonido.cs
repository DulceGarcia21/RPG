using UnityEngine;
using UnityEngine.UI;

public class ControladorSonido : MonoBehaviour
{
    public Slider SlVol;
    public AudioSource reproductor;
    public Button btnApagarSnd;
    public Sprite snd0, snd1;

    public static float volumen; //vol: 0-1
    public static bool statusAudio = true; //encendio o apagado

    void Start()
    {
        if(volumen == 0){
            Debug.Log("Sin modificar volumen inicial");
        }else{
            Debug.Log("Vol: "+volumen.ToString());
            reproductor.volume = volumen; //Asigna los valoresd evolumen al regresar del juego
            SlVol.value = volumen;
        }
        if(statusAudio == false){
            reproductor.Stop();
            btnApagarSnd.image.sprite = snd1;
        }else{
            btnApagarSnd.image.sprite = snd0;
        }
    }

    public void ModificaVolumen(){
        volumen = SlVol.value;
        reproductor.volume = volumen;
    }

    public void ActivaAudio(){
        if(statusAudio == true){
            reproductor.Stop();
            statusAudio = false;
            btnApagarSnd.image.sprite = snd1;
        }else{
            reproductor.Play();
            statusAudio = true;
            btnApagarSnd.image.sprite = snd0;
        }   
    }

}
