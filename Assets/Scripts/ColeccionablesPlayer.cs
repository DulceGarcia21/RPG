using UnityEngine;

public class ColeccionablesPlayer : MonoBehaviour
{
    public Inventario inventario;

    private void Start()
    {
        if (inventario == null)
        {
            inventario = FindAnyObjectByType<Inventario>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Recogibles"))
        {
            ItemControlador itemCtrl = collision.GetComponent<ItemControlador>();
            if (itemCtrl != null && itemCtrl.item != null)
            {
                if (inventario != null && inventario.AgregarItem(itemCtrl.item))
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}