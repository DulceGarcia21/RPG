using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Posiciones de destino")]
    public Vector3 posicionCamaraDestino;
    public Vector3 posicionPlayerDestino;

    [Header("Referencias (Opcional, si no las encuentra auto)")]
    public Camera camara;

    private void Start()
    {
        // Si no asignaste la cámara manualmente, la busca automáticamente en la escena
        if (camara == null)
        {
            camara = Camera.main;
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        // Verifica que quien pisó el portal sea el Jugador
        if (obj.CompareTag("Player"))
        {
            // Mueve la cámara
            camara.transform.position = posicionCamaraDestino;

            // Mueve al jugador (obj es el Collider del jugador)
            obj.transform.position = posicionPlayerDestino;
        }
    }
}