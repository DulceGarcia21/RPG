using UnityEngine;
using UnityEngine.AI; 

public class Enemigo : MonoBehaviour
{
    public int vidaEnemigo = 1;
    public Transform personaje;
    public Transform[] puntosRuta;
    private int indiceRuta = 0;
    private bool playerEnRango = false;
    
    [SerializeField] private float distanciaDeteccionPlayer = 5f; // Valor por defecto ajustable
    
    private SpriteRenderer spriteEnemigo;
    private NavMeshAgent agente; 
    private Transform mirarHacia;

    private float frecAtaque = 2.5f, tiempoSigAtaque = 0, iniciaConteo;

    private void Awake(){
        agente = GetComponent<NavMeshAgent>();
        spriteEnemigo = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        vidaEnemigo = 1;
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    }

    void Update (){
        // Forzamos que Z siempre sea 0 en 2D
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
        float distancia = Vector3.Distance(personaje.position, transform.position);

        // L0gica de patrullaje por puntos si no sigue al jugador
        if(puntosRuta.Length > 0 && transform.position == puntosRuta[indiceRuta].position){
            if(indiceRuta < puntosRuta.Length - 1){
                indiceRuta++;
            }else if(indiceRuta == puntosRuta.Length - 1){
                indiceRuta = 0;
            }
        }

        // Deteccion del jugador
        if(distancia < distanciaDeteccionPlayer){
            playerEnRango = true;
        }else{
            playerEnRango = false;
        }

        if(tiempoSigAtaque > 0){
            tiempoSigAtaque = frecAtaque + iniciaConteo - Time.time;
        }else{
            tiempoSigAtaque = 0;
            VidasPlayer.puedePerderVida = 1;
            
        }
        SigueAlPlayer(playerEnRango);
        RotaEnemigo();
    }

    private void SigueAlPlayer(bool playerEnRango){
        if(playerEnRango){
            agente.SetDestination(personaje.position); 
            mirarHacia = personaje;
        }else if(puntosRuta.Length > 0){
            agente.SetDestination(puntosRuta[indiceRuta].position);
            mirarHacia = puntosRuta[indiceRuta];
        }
    }

    private void RotaEnemigo(){
        if(agente.velocity.x > 0.1f){
            spriteEnemigo.flipX = false; // Mirando a la derecha
        }else if(agente.velocity.x < -0.1f){
            spriteEnemigo.flipX = true;  // Mirando a la izquierda
        }
    }

    private void OnTriggerStay2D(Collider2D obj){
        if(obj.CompareTag("Player")){
            VidasPlayer vidasPlayer = obj.transform.GetComponent<VidasPlayer>();
            if(vidasPlayer != null){
                if(tiempoSigAtaque <= 0){
                    tiempoSigAtaque = frecAtaque;
                    iniciaConteo = Time.time;
                    vidasPlayer.TomarDanio(1);
                }
            }
        }
    }

    public void TomarDanio(int danio){
        vidaEnemigo -= danio;
        if(vidaEnemigo <= 0){
            Destroy(gameObject);
        }
    }
}
