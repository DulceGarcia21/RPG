using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vidaEnemigo = 1;
    private float frecAtaque = 2.5f, tiempoSigAtaque = 0, iniciaConteo;

    void Start()
    {
        vidaEnemigo = 1;
    }


    private void OnTriggerStay2D(Collider2D obj){
        if(obj.tag == "Player"){
            VidasPlayer vidasPlayer = obj.transform.GetComponent<VidasPlayer>();
            if(vidasPlayer != null){
                vidasPlayer.TomarDanio(1);
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
