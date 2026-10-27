using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    
    public GameObject inventarioUI;
    public Transform contenedorCasillas;
    private bool muestraInventario = false;

    public ItemData[] slots = new ItemData[20];
    public VidasPlayer vidasPlayer;
    public EscudoMagico escudoMagico;
    public AudioClip sonidoInventario; 
    private AudioSource audioSource;

    void Start()
    {
        muestraInventario = false;
        if (inventarioUI != null){
            inventarioUI.SetActive(false);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null){
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        if (vidasPlayer == null) vidasPlayer = FindFirstObjectByType<VidasPlayer>();
        if (escudoMagico == null) escudoMagico = FindFirstObjectByType<EscudoMagico>();

        ActualizarUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)){
            EstatusInventario();
        }
    }

    public void EstatusInventario(){
        muestraInventario = !muestraInventario;
        if (inventarioUI != null){
            inventarioUI.SetActive(muestraInventario);
        }

        if (sonidoInventario != null && audioSource != null){
            audioSource.PlayOneShot(sonidoInventario);
        }

        Time.timeScale = muestraInventario ? 0f : 1f;
    }

    public bool AgregarItem(ItemData nuevoItem)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = nuevoItem;
                ActualizarUI();
                Debug.Log("Item '" + nuevoItem.nombreItem + "' guardado en la casilla " + i);
                return true;
            }
        }
        Debug.LogWarning("Inventario lleno");
        return false;
    }

    public void UsarItem(int indice){
        if (indice < 0 || indice >= slots.Length || slots[indice] == null) return;

        ItemData item = slots[indice];

        switch (item.tipo){
            case TipoItem.PocionVida:
                if (vidasPlayer != null)
                {
                    vidasPlayer.CurarVida(item.valorVida);
                    slots[indice] = null; // Consume la pocion
                }
                break;

            case TipoItem.PocionMana:
                if (escudoMagico != null)
                {
                    escudoMagico.RecargarManaPorcentaje(item.porcentajeMana);
                    slots[indice] = null; // Consume la pocion
                }
                break;

            case TipoItem.Coleccionable:
                Debug.Log("Este item es un coleccionable y no se consume.");
                break;
        }

        ActualizarUI();
    }

    public void ActualizarUI()
    {
        if (contenedorCasillas == null) return;

        for (int i = 0; i < contenedorCasillas.childCount; i++)
        {
            Transform casilla = contenedorCasillas.GetChild(i);
            Image iconoImagen = casilla.Find("Icono")?.GetComponent<Image>();

            if (i < slots.Length && slots[i] != null){
                if (iconoImagen != null){
                    iconoImagen.sprite = slots[i].icono;
                    iconoImagen.enabled = true;
                }
            }else{
                if (iconoImagen != null){
                    iconoImagen.enabled = false;
                }
            }
        }
    }
}