using UnityEngine;

public class CAD : MonoBehaviour
{
    [SerializeField] private GameObject proyectil;
    public float tiempoEntreAtaques;
    public float tiempoSigAtaque;
    private Animator anim;
    public Transform puntoEmision;

    public static bool disparando = false;
    public static int dirDisparo = 0;
    [SerializeField] private AudioClip sonidoArco;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tiempoSigAtaque < 0.05f && tiempoEntreAtaques >0){
            disparando = false;
        }
        if(tiempoSigAtaque > 0){
            tiempoSigAtaque -= Time.deltaTime;
        }
        if(Input.GetButtonDown("Fire2") && tiempoSigAtaque <= 0){
            disparando = true;
            activaCapa("Ataque");
            Dispara();
            tiempoSigAtaque = tiempoEntreAtaques;
        }
    }

    private void Dispara(){
        switch(MovPlayer.dirAtaque){
            case 1: // Frente
            case 7: // Abajo-Derecha 
            case 8: // Abajo-Izquierda
                anim.SetTrigger("disparaFront");        
                break;
                
            case 2: // Espalda
            case 5: // Arriba-Derecha
            case 6: // Arriba-Izquierda 
                anim.SetTrigger("disparaBack");        
                break;
                
            case 3: // Izquierda
                anim.SetTrigger("disparaLeft");        
                break;
                
            case 4: // Derecha
                anim.SetTrigger("disparaRight");        
                break;
        }
    }

    private void EmiteProyectil(){
        if (sonidoArco != null){
            AudioSource.PlayClipAtPoint(sonidoArco, transform.position);
        }

        dirDisparo = MovPlayer.dirAtaque;
        Instantiate(proyectil, puntoEmision.position, transform.rotation);
    }
    
    private void activaCapa(string nombre){
        for(int i = 0; i < anim.layerCount; i++){
            anim.SetLayerWeight(i, 0); 
        }
        anim.SetLayerWeight(anim.GetLayerIndex(nombre), 1);
    }
}
