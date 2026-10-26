using UnityEngine;

public class Inventario : MonoBehaviour
{
    [Header("UI y Visibilidad")]
    public GameObject inventarioUI;
    private bool muestraInventario = false;

    [Header("Estructura de Datos")]
    private string[] valoresInventario = new string[20];

    void Start()
    {
        // Fuerza el estado inicial oculto al arrancar la partida
        muestraInventario = false;

        if (inventarioUI != null)
        {
            inventarioUI.SetActive(false);
        }
        else
        {
            Debug.LogError("¡Atención! No has asignado el GameObject 'FondoInventario' en el campo 'Inventario UI' del Inspector.");
        }

        // Asegura que el juego inicie con la velocidad del tiempo normal
        Time.timeScale = 1f;

        BorraArreglo();
    }

    void Update()
    {
        // Detecta la tecla 'I' para abrir o cerrar
        if (Input.GetKeyDown(KeyCode.I))
        {
            EstatusInventario();
        }
    }

    // Método asignado a la tecla 'I', al botón de abrir y a la 'X' de cierre
    public void EstatusInventario()
    {
        muestraInventario = !muestraInventario;

        if (inventarioUI != null)
        {
            inventarioUI.SetActive(muestraInventario);
        }

        // Si se muestra el inventario, congela el juego (Time.timeScale = 0).
        // Si se oculta, reanuda el tiempo normal (Time.timeScale = 1).
        Time.timeScale = muestraInventario ? 0f : 1f;
    }

    public void EscribeEnArreglo()
    {
        for (int i = 0; i < valoresInventario.Length; i++)
        {
            if (string.IsNullOrEmpty(valoresInventario[i]))
            {
                valoresInventario[i] = ColeccionablesPlayer.objetoAColeccionar;
                Debug.Log("Objeto '" + valoresInventario[i] + "' guardado en la casilla " + i);

                if (!muestraInventario)
                {
                    EstatusInventario();
                }
                break;
            }
        }
    }

    private void BorraArreglo()
    {
        for (int i = 0; i < valoresInventario.Length; i++)
        {
            valoresInventario[i] = "";
        }
    }
}