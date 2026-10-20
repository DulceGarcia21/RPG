using UnityEngine;

public class MovPlayer : MonoBehaviour
{
    private Vector2 dirMov;
    public float velMov;
    public Rigidbody2D rb;
    public Animator anim;

    private bool PlayerMoviendose = false;
    private float ultimoMovX, ultimoMovY;
    public static int dirAtaque = 0; //1-Front, 2-Back, 3-Left, 4-Right

    // Update is called once per frame
    void FixedUpdate(){
        Movimiento();
        if(CCC.atacando == false && CAD.disparando == false){
            AnimacionesPlayer();  
        }
    }

    private void Movimiento(){
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");
        dirMov = new Vector2(movX, movY).normalized;
        rb.linearVelocity = new Vector2(dirMov.x * velMov, dirMov.y * velMov);

        if (movX == 1 && movY == 1) {
            dirAtaque = 5; // Arriba-Derecha
        } else if (movX == -1 && movY == 1) {
            dirAtaque = 6; // Arriba-Izquierda
        } else if (movX == 1 && movY == -1) {
            dirAtaque = 7; // Abajo-Derecha
        } else if (movX == -1 && movY == -1) {
            dirAtaque = 8; // Abajo-Izquierda
        } else if(movX == -1){
            dirAtaque = 3; // Izquierda
        } else if(movX == 1){
            dirAtaque = 4; // Derecha
        } else if(movY == -1){
            dirAtaque = 1; // Frente / Abajo
        } else if(movY == 1){
            dirAtaque = 2; // Espalda / Arriba
        }

        if(movX == 0 && movY == 0){  // Idle
            PlayerMoviendose = false;
        }else{ // Caminar
            PlayerMoviendose = true;
            ultimoMovX = movX;
            ultimoMovY = movY;
        }

        ActualizarCapa();
    }

    private void AnimacionesPlayer(){
        anim.SetFloat("movX", ultimoMovX);
        anim.SetFloat("movY", ultimoMovY);
    }

    private void ActualizarCapa(){
        if(CCC.atacando == false && CAD.disparando == false){
            if(PlayerMoviendose == true){ 
                activaCapa("Caminar");
            }else{
                activaCapa("Idle");
            }
        }else{
            activaCapa("Ataque");
        }
    }

    private void activaCapa(string nombre){
        for(int i = 0; i < anim.layerCount; i++){
            anim.SetLayerWeight(i, 0); 
        }
        anim.SetLayerWeight(anim.GetLayerIndex(nombre), 1);
    }
}


