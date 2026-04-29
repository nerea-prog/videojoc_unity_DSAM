using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 9f;
    public float jumpForce = 9f;

    private float moveX;

    public bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // inicializacion de la capsula
    }

    // lectura de lo que pulsamos en tecladp
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>();
        moveX = moveInput.x;
    }

    public void OnJump(InputValue value)
    {
        if (isGrounded)
        {   // salta manteniendo la velocidad horizontal (x), que se deja a la fisica
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {   
        // controlamos solo movimiento horizontal (x), vertical (y) se deja a la fisica
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))  // si el objeto choca con tierra
        {
            isGrounded = true; // es tierra
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) // si l'objecte que esta chocant es terra
        {
            {
                isGrounded = false; // no es tierra
            }
        }
    }
}