using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [Header("UI de Diálogo")]
    public GameObject cajaDialogo;
    public TextMeshProUGUI textoTMP;

    [Header("Velocidad de Texto")]
    [Tooltip("Tiempo en segundos entre cada letra")]
    public float velocidadEscritura = 0.03f;

    [Header("Líneas de Texto del NPC")]
    [TextArea(2, 5)]
    public List<string> dialogos = new List<string>()
    {
        "¡Por fin despiertas, Darrow! Pensé que te habías quedado dormido.",
        "Las patrullas de Ceres ya están cercando el paso del bosque.",
        "Debes tener cuidado si vas a salir ahora."
    };

    private bool jugadorCerca = false;
    private int indiceDialogo = 0;
    private bool estaEscribiendo = false;
    private Coroutine corrutinaTexto;

    void Start()
    {
        if (cajaDialogo != null)
        {
            cajaDialogo.SetActive(false);
        }
        indiceDialogo = 0;
    }

    void Update()
    {

        if (jugadorCerca && Input.GetKeyDown(KeyCode.Space))
        {

            if (estaEscribiendo)
            {
                CompletarTextoDeInmediato();
            }

            else
            {
                indiceDialogo++;
                MostrarSiguienteLinea();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            jugadorCerca = true;


            cajaDialogo.SetActive(true);
            indiceDialogo = 0;
            MostrarSiguienteLinea();
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            jugadorCerca = false;
            ReiniciarDialogo(); 
        }
    }

    private void MostrarSiguienteLinea()
    {

        if (indiceDialogo < dialogos.Count)
        {
            if (corrutinaTexto != null)
            {
                StopCoroutine(corrutinaTexto);
            }
            corrutinaTexto = StartCoroutine(EscribirTexto(dialogos[indiceDialogo]));
        }
        else
        {
            ReiniciarDialogo();
        }
    }


    IEnumerator EscribirTexto(string linea)
    {
        estaEscribiendo = true;
        textoTMP.text = "";

        foreach (char letra in linea.ToCharArray())
        {
            textoTMP.text += letra;
            RefrescarTamañoCaja();
            yield return new WaitForSeconds(velocidadEscritura);
        }

        estaEscribiendo = false;
    }

    private void CompletarTextoDeInmediato()
    {
        if (corrutinaTexto != null)
        {
            StopCoroutine(corrutinaTexto);
        }
        textoTMP.text = dialogos[indiceDialogo];
        RefrescarTamañoCaja();
        estaEscribiendo = false;
    }

    private void RefrescarTamañoCaja()
    {
        if (cajaDialogo != null)
        {
            RectTransform rectCaja = cajaDialogo.GetComponent<RectTransform>();
            if (rectCaja != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(rectCaja);
            }
        }
    }

    public void ReiniciarDialogo()
    {
        if (corrutinaTexto != null)
        {
            StopCoroutine(corrutinaTexto);
        }
        estaEscribiendo = false;
        indiceDialogo = 0;

        if (cajaDialogo != null)
        {
            cajaDialogo.SetActive(false);
        }
    }
}