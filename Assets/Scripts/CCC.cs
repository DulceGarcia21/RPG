using UnityEngine;

public class CCC : MonoBehaviour
{
    public Transform controladorGolpe;
    public float radioGolpe;
    public int danioGolpe;
    public float tiempoEntreAtaques;
    public float tiempoSigAtaque;
    private Animator anim;

    public static bool atacando;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tiempoSigAtaque < 0.05f && tiempoEntreAtaques >0){
            atacando = false;
        }
        if(tiempoSigAtaque > 0){
            tiempoSigAtaque -= Time.deltaTime;
        }
        if(Input.GetButtonDown("Fire1") && tiempoSigAtaque <= 0){
            atacando = true;
            activaCapa("Ataque");
            Golpe();
            tiempoSigAtaque = tiempoEntreAtaques;
        }
    }

    private void Golpe(){
        if(MovPlayer.dirAtaque == 1){
            anim.SetTrigger("ataqueFront");        
        }else if(MovPlayer.dirAtaque == 2){
            anim.SetTrigger("ataqueBack");        
        }else if(MovPlayer.dirAtaque == 3){
            anim.SetTrigger("ataqueLeft");        
        }else if(MovPlayer.dirAtaque == 4){
            anim.SetTrigger("ataqueRight");        
        }
    }

    private void VerificaGolpe(){
        Collider2D[] objs = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe); //Verifica que Golpeo
        foreach (Collider2D colisionador in objs){
            if(colisionador.CompareTag("enemigo")){
                colisionador.transform.GetComponent<Enemigo>().TomarDanio(danioGolpe);
            }
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }

    private void activaCapa(string nombre){
        for(int i = 0; i < anim.layerCount; i++){
            anim.SetLayerWeight(i, 0); 
        }
        anim.SetLayerWeight(anim.GetLayerIndex(nombre), 1);
    }

}
