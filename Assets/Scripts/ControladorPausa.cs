using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorPausa : MonoBehaviour
{
   public GameObject panelPausa;
   private bool estaPausado = false;

    void Start(){
        Time.timeScale = 1;
        panelPausa.SetActive(false);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (estaPausado){
                CerrarPausa();
            }else{
                AbrirPausa();
            }
        }
    }

    private void CerrarPausa(){
        panelPausa.SetActive(false);
        Time.timeScale = 1;
        estaPausado = false;
    }

    private void AbrirPausa(){
        panelPausa.SetActive(true);
        Time.timeScale = 0;
        estaPausado = true;
    }

    public void AlternarPausa(){
        if (estaPausado){
            CerrarPausa();
        }else{
            AbrirPausa();
        }
    }
    public void BotonPausa(){
        AlternarPausa();
    }

    public void IrAMenu(){
        CerrarPausa();
        SceneManager.LoadScene("Inicio");
    }
}
