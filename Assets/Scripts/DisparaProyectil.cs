using UnityEngine;

public class DisparaFlecha : MonoBehaviour
{
    [SerializeField] private float velocidad = 8.0f;
    private Vector3 direccionMovimiento;

    void Start(){
        switch (CAD.dirDisparo)
        {
            case 1:  direccionMovimiento = new Vector3(0, -1, 0); break;
            case 2:  direccionMovimiento = new Vector3(0, 1, 0); break;
            case 3:  direccionMovimiento = new Vector3(-1, 0, 0); break;
            case 4:  direccionMovimiento = new Vector3(1, 0, 0); break;
            case 5:  direccionMovimiento = new Vector3(1, 1, 0).normalized; break;
            case 6:  direccionMovimiento = new Vector3(-1, 1, 0).normalized; break;
            case 7:  direccionMovimiento = new Vector3(1, -1, 0).normalized; break;
            case 8:  direccionMovimiento = new Vector3(-1, -1, 0).normalized; break;
        }
    }
    void FixedUpdate(){
        transform.position += direccionMovimiento * Time.deltaTime * velocidad;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "limites"){
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "enemigo"){
            collision.transform.GetComponent<Enemigo>().TomarDanio(1);
            Destroy(this.gameObject);
        }
    }
}
