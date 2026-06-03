using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb2d;

    [Header("Movement")]
    public float acceleration;
    public float deceleration;
    public float topSpeed;

    private float directionX;
    public Vector2 jumpBoxSize;
    public float jumpBoxDistance;
    public float jumpForce;
    public LayerMask groundLayer;

    public void FixedUpdate()
    {
        Vector2 movementVelocity = new Vector2(directionX * topSpeed, 0);
        float currentSpeedRate = (movementVelocity.magnitude > 0) ? acceleration : deceleration;
        rb2d.linearVelocityX = Vector2.MoveTowards(rb2d.linearVelocity, movementVelocity, currentSpeedRate * Time.fixedDeltaTime).x;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        directionX = ctx.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded())
        {
            rb2d.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
    }
    private bool isGrounded()
    {
        return Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y - jumpBoxDistance), jumpBoxSize, 0, Vector2.up, 0, groundLayer);
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - jumpBoxDistance), jumpBoxSize);
    }
}
