using UnityEngine;

public class MovCamara : MonoBehaviour
{
    public Camera camara;

    private void OnTriggerEnter2D(Collider2D obj){
        if(obj.gameObject.CompareTag("portal1")){
            Vector3 posicioncamara = new Vector3(-33.46f, 0f, -10f); 
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(-22f, -1.72f, 0f);
            this.transform.position = posicionPlayer;
        }

        if(obj.gameObject.CompareTag("portal2")){
            Vector3 posicioncamara = new Vector3(0f, 0f, -10f);
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(-11.5f, -1.72f, 0f);
            this.transform.position = posicionPlayer;
        }
    }
}
