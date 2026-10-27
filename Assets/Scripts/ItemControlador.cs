using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemControlador : MonoBehaviour
{
    public ItemData item;

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null && item != null)
        {
            sr.sprite = item.icono;
        }
    }
}