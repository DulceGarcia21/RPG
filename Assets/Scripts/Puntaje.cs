using UnityEngine;
using TMPro;

public class Puntaje : MonoBehaviour
{
    public int puntajeActual = 0;
    public TMP_Text textoPuntaje;
    public static Puntaje instancia;

    void Awake(){
        if (instancia != null && instancia != this){
            Destroy(gameObject);
            return;
        }
        instancia = this;
    }

    void Start()
    {
        ActualizarInterfaz();
    }

    public void SumarPuntos(int cantidad){
        puntajeActual += cantidad;
        ActualizarInterfaz();
    }
    private void ActualizarInterfaz(){
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntos: " + puntajeActual;
        }
    }
}
