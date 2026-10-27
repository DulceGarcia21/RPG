using UnityEngine;

public enum TipoItem
{
    PocionVida,
    PocionMana,
    Coleccionable
}

[CreateAssetMenu(fileName = "NuevoItem", menuName = "Inventario/ItemData")]
public class ItemData : ScriptableObject
{
    public string nombreItem;
    public Sprite icono;
    public TipoItem tipo;

    [Header("Efectos (Si aplica)")]
    public int valorVida;
    public float porcentajeMana;
}