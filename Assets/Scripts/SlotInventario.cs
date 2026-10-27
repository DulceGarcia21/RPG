using UnityEngine;
using UnityEngine.EventSystems;

public class SlotInventario : MonoBehaviour, IPointerClickHandler
{
    public int indiceCasilla;
    public Inventario inventario;

    private void Start()
    {
        if (inventario == null)
        {
            inventario = FindAnyObjectByType<Inventario>();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventario != null)
        {
            inventario.UsarItem(indiceCasilla);
        }
    }
}