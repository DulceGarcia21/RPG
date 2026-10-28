using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorSonido : MonoBehaviour
{
    public Slider SlVol;
    public AudioSource reproductor;
    public Button btnApagarSnd;
    public Sprite snd0, snd1;

    public static float volumen = 1f; //vol: 0-1
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
        ConfigurarReproductor();
        InicializarSlider();
        ActualizarEstadoVisualBotones();
    }

    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        ConfigurarReproductor();
        
        // Buscamos el slider en la nueva escena 
        if (SlVol == null){
            Slider sliderEnEscena = FindAnyObjectByType<Slider>();
            if (sliderEnEscena != null){
                SlVol = sliderEnEscena;
                SlVol.value = volumen;
                SlVol.onValueChanged.RemoveAllListeners();
                SlVol.onValueChanged.AddListener(_ => ModificaVolumen());
            }
        }

        Button[] todosLosBotones = Resources.FindObjectsOfTypeAll<Button>();
        foreach (Button boton in todosLosBotones)
        {
            // Validamos que pertenezca a la escena actual y se llame exactamente como el botón de audio
            if (boton.gameObject.scene.IsValid() && boton.name == "ApagarEncenderVol")
            {
                btnApagarSnd = boton;
                btnApagarSnd.onClick.RemoveAllListeners();
                btnApagarSnd.onClick.AddListener(ActivaAudio);
                break;
            }
        }
        
        ActualizarEstadoVisualBotones();
    }

    private void ConfigurarReproductor()
    {
        if (reproductor == null){
            reproductor = GetComponent<AudioSource>();
            if (reproductor == null){
                reproductor = gameObject.AddComponent<AudioSource>();
            }
        }
        reproductor.volume = volumen;
        reproductor.mute = !statusAudio; // Mantiene el estado de silencio entre escenas
    }

    private void InicializarSlider()
    {
        if (SlVol != null){
            SlVol.value = volumen;
            SlVol.onValueChanged.RemoveAllListeners();
            SlVol.onValueChanged.AddListener(_ => ModificaVolumen());
        }
    }

    public void ModificaVolumen(){
        if (SlVol != null){
            volumen = SlVol.value;
            if (reproductor != null)
            {
                reproductor.volume = volumen;
            }
        }
    }

    public void ActivaAudio(){
        statusAudio = !statusAudio;
        
        if (reproductor != null)
        {
            reproductor.mute = !statusAudio;
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