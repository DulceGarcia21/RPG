using UnityEngine;

public class DialogoNPC : MonoBehaviour
{
    private void OnMouseDown()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        // Se oculta al presionar Espacio, Enter o la tecla E  
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
        }
    }
}