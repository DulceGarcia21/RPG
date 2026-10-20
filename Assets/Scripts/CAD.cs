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
        if(MovPlayer.dirAtaque == 1){
            anim.SetTrigger("disparaFront");        
        }else if(MovPlayer.dirAtaque == 2){
            anim.SetTrigger("disparaBack");        
        }else if(MovPlayer.dirAtaque == 3){
            anim.SetTrigger("disparaLeft");        
        }else if(MovPlayer.dirAtaque == 4){
            anim.SetTrigger("disparaRight");        
        }
    }

    private void EmiteProyectil(){
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
