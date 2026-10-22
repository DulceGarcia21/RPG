using UnityEngine;
using UnityEngine UI;
using UnityEngine.SceneManagment;

public class ControladorMenu : MonoBehaviour
{
    public GameObject panelOpciones

    void Start()
    {
        panelOpciones.SetActive(false);

    }

    public void Jugar(){
        SceneManager.LoadScene("Main");
    }

    public void AbreOpciones(){
        panelOpciones.SetActive(true)
    }

    public void CierraOpciones(){
        panelOpciones.SetActive(false)
    }

    public void SalirJugeo(){
        Application.Quit;
    }
}
