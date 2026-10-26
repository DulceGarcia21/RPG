using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour

{
    public int vidaEnemigo = 1;
    private int vidaActual;
    public int danioJugador = 1;
    public Transform personaje;
    public Transform[] puntosRuta;
    private int indiceRuta = 0;
    private bool playerEnRango = false;

    [SerializeField] private float distanciaDeteccionPlayer = 5f;
    [SerializeField] private Image barraVidaImagen;
    private float anchoBarraOriginal;

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
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        vidaActual = vidaEnemigo;

        if(barraVidaImagen != null){
            anchoBarraOriginal = barraVidaImagen.GetComponent<RectTransform>().sizeDelta.x;
        }
    }

    void Update (){
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        float distancia = Vector3.Distance(personaje.position, transform.position);
        if(puntosRuta.Length > 0 && Vector3.Distance(transform.position, puntosRuta[indiceRuta].position) < 0.2f){
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

        if (tiempoSigAtaque > 0){
            tiempoSigAtaque = frecAtaque + iniciaConteo - Time.time;
        }else{
            tiempoSigAtaque = 0;
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
                if(vidasPlayer != null && tiempoSigAtaque <= 0){
                    tiempoSigAtaque = frecAtaque;
                    iniciaConteo = Time.time;
                    vidasPlayer.TomarDanio(danioJugador);
                }
            }
        }
    }

    public void TomarDanio(int danio){
        vidaActual -= danio;
        if (vidaActual < 0) {
            vidaActual = 0;
        }
        ActualizarBarraVida();

        if(vidaActual <= 0){
            Destroy(gameObject);
        }
    }

    private void ActualizarBarraVida(){
        if(barraVidaImagen != null){
            RectTransform rectTransform = barraVidaImagen.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(anchoBarraOriginal * (float)vidaActual / (float)vidaEnemigo, rectTransform.sizeDelta.y);
        }
    }
}
