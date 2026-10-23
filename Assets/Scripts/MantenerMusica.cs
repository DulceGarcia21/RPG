using UnityEngine;

public class MantenerMusica : MonoBehaviour
{
    private static MantenerMusica instancia;

    void Awake(){
        if (instancia != null && instancia != this){
            Destroy(gameObject);
            return;
        }
        instancia = this;
        DontDestroyOnLoad(gameObject);
    }
}
