using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb2d;

    [Header("Movement")]
    public float acceleration;
    public float topSpeed;
    public float maxSpeed;

    private float directionX;

    public void FixedUpdate()
    {
        Vector2 movementVelocity = new Vector2(directionX * acceleration, 0);
        rb2d.AddRelativeForce(movementVelocity);
        if (movementVelocity.magnitude > topSpeed)
            movementVelocity = movementVelocity.normalized * topSpeed;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        directionX = ctx.ReadValue<Vector2>().x;
    }
}
