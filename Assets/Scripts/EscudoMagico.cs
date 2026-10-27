using UnityEngine;
using UnityEngine.UI;

public class EscudoMagico : MonoBehaviour
{
    public GameObject objetoEscudo;
    public float manaMaximo = 100f;
    public float manaActual;
    public float costoManaPorSegundo = 25f;
    public float velocidadRegeneracion = 5f;
    public Image barraManaImagen;

    private float anchoBarraOriginal;
    private bool escudoActivo = false;

    void Start()
    {
        manaActual = manaMaximo;
        if (objetoEscudo != null)
        {
            objetoEscudo.SetActive(false);
        }
        if (barraManaImagen != null)
        {
            anchoBarraOriginal = barraManaImagen.GetComponent<RectTransform>().sizeDelta.x;
        }
        ActualizarBarraMana();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && manaActual > 0)
        {
            ActivarEscudo();
        }
        else
        {
            DesactivarEscudo();
            if (manaActual < manaMaximo)
            {
                manaActual += velocidadRegeneracion * Time.deltaTime;
                if (manaActual > manaMaximo) manaActual = manaMaximo;
                ActualizarBarraMana();
            }
        }
    }

    void ActivarEscudo()
    {
        escudoActivo = true;
        if (objetoEscudo != null && !objetoEscudo.activeSelf)
        {
            objetoEscudo.SetActive(true);
        }
        manaActual -= costoManaPorSegundo * Time.deltaTime;
        if (manaActual <= 0)
        {
            manaActual = 0;
            DesactivarEscudo();
        }
        ActualizarBarraMana();
    }

    void DesactivarEscudo()
    {
        escudoActivo = false;
        if (objetoEscudo != null && objetoEscudo.activeSelf)
        {
            objetoEscudo.SetActive(false);
        }
    }

    // MÉTODO PARA EL INVENTARIO: Recarga maná por porcentaje al consumir pociones
    public void RecargarManaPorcentaje(float porcentaje)
    {
        manaActual += manaMaximo * porcentaje;
        if (manaActual > manaMaximo)
        {
            manaActual = manaMaximo;
        }
        ActualizarBarraMana();
        Debug.Log("Maná recargado: +" + (porcentaje * 100) + "% | Maná actual: " + manaActual);
    }

    private void ActualizarBarraMana()
    {
        if (barraManaImagen != null)
        {
            RectTransform rectTransform = barraManaImagen.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(anchoBarraOriginal * (manaActual / manaMaximo), rectTransform.sizeDelta.y);
        }
    }

    public bool EstaActivo()
    {
        return escudoActivo;
    }
}