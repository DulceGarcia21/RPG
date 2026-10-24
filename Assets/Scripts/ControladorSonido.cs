using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorSonido : MonoBehaviour
{
    public Slider SlVol;
    public AudioSource reproductor;
    public Button btnApagarSnd;
    public Sprite snd0, snd1;

    public static float volumen; //vol: 0-1
    public static bool statusAudio = true; //encendio o apagado
    private static ControladorSonido instancia;

    void Awake(){
        if (instancia != null && instancia != this) {
            Destroy(gameObject);
            return;
        }
        instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start(){
        if (reproductor == null) reproductor = GetComponent<AudioSource>();
        reproductor.volume = volumen;
        if (SlVol != null) SlVol.value = volumen;
        ActualizarEstadoVisualBotones();
    }

    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if (SlVol == null){
            Slider sliderEnEscena = FindAnyObjectByType<Slider>();
            if (sliderEnEscena != null){
                SlVol = sliderEnEscena;
                SlVol.value = volumen;
                SlVol.onValueChanged.AddListener(_ => ModificaVolumen());
            }
        }
        GameObject objBoton = GameObject.Find("ApagarEncenderVol"); 
        if (objBoton != null)
        {
            btnApagarSnd = objBoton.GetComponent<Button>();
            btnApagarSnd.onClick.RemoveAllListeners();
            btnApagarSnd.onClick.AddListener(ActivaAudio);
        }
    }

    public void ModificaVolumen(){
        if (SlVol != null){
            volumen = SlVol.value;
            reproductor.volume = volumen;
        }
    }

    public void ActivaAudio(){
        if (statusAudio){
            reproductor.Stop();
            statusAudio = false;
        }
        else{
            reproductor.Play();
            statusAudio = true;
        }
        ActualizarEstadoVisualBotones(); 
    }
    private void ActualizarEstadoVisualBotones(){
        if (btnApagarSnd != null && snd0 != null && snd1 != null)
        {
            btnApagarSnd.image.sprite = statusAudio ? snd0 : snd1;
        }
    }

}
