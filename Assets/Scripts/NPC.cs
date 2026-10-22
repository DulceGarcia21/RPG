using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject txtDialogo;
    public static int numVisitas;

    public Sprite txt1;
    public Sprite txt2;

    void Start()
    {
        if (txtDialogo != null)
        {
            txtDialogo.SetActive(false);
        }
        numVisitas = 0;
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
          //aca aprovecho la tag player pa que solo reaccione con esa
        if (obj.CompareTag("Player"))
        {
            if (txtDialogo != null)
            {
                txtDialogo.SetActive(true);
                EscribeDialogo();
                numVisitas++;
            }
        }
    }

    private void EscribeDialogo()
    {
        SpriteRenderer sr = txtDialogo.GetComponent<SpriteRenderer>();

        if (sr != null)
        {
            if (numVisitas == 0 && txt1 != null)
            {
                sr.sprite = txt1;
            }
                //esto fue pq como tenia dos colaiders medio se trababa tons hacia una doble comprobacion perooooo ahora lo voy a usar pa que cambie el texto
            else if (txt2 != null)
            {
                sr.sprite = txt2;
            }
        }
    }
}