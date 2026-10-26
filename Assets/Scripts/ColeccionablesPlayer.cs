using UnityEngine;

public class ColeccionablesPlayer : MonoBehaviour
{
    public static string objetoAColeccionar = "";
    private Inventario inventario;

    void Start()
    {
        objetoAColeccionar = "";

        inventario = Object.FindFirstObjectByType<Inventario>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.CompareTag("Recogibles"))
        {

            objetoAColeccionar = collision.gameObject.name;

            if (inventario != null)
            {
                inventario.EscribeEnArreglo();
            }

            // Destruye el objeto de la escena
            Destroy(collision.gameObject);
        }
    }
}