using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb2d;

    [Header("GameObjects")]
    public GameObject portal1;
    public GameObject portal2;

    [Header("Movement")]
    public float acceleration;
    public float deceleration;
    public float topSpeed;
    public float maxSpeed;

    private float directionX;
    public Vector2 jumpBoxSize;
    public float jumpBoxDistance;
    public float jumpForce;
    public LayerMask groundLayer;

    [Header("Portals")]
    private Vector2 mousePos;

    public void FixedUpdate()
    {
        Vector2 movementVelocity = new Vector2(directionX * topSpeed, 0);
        float currentSpeedRate = (movementVelocity.magnitude > 0) ? acceleration : deceleration;
        rb2d.linearVelocityX = Vector2.MoveTowards(rb2d.linearVelocity, movementVelocity, currentSpeedRate * Time.fixedDeltaTime).x;
        if(rb2d.linearVelocity.magnitude > maxSpeed)
        {
            rb2d.linearVelocity = Vector2.ClampMagnitude(rb2d.linearVelocity, maxSpeed);
        }
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
    public void PlacePortal1(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Portal[] portals = FindObjectsByType<Portal>(FindObjectsSortMode.None);
            for (int i = 0; i < portals.Length; i++)
            {
                if (portals[i].gameObject.CompareTag("Portal1"))
                    Destroy(portals[i].gameObject);
            }
            Instantiate(portal1, mousePos, Quaternion.Euler(Vector3.zero));
        }
    }
    public void PlacePortal2(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Portal[] portals = FindObjectsByType<Portal>(FindObjectsSortMode.None);
            for (int i = 0; i < portals.Length; i++)
            {
                if (portals[i].gameObject.CompareTag("Portal2"))
                    Destroy(portals[i].gameObject);
            }
            Instantiate(portal2, mousePos, Quaternion.Euler(Vector3.zero));
        }
    }
    public void MoveMouse(InputAction.CallbackContext ctx)
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(ctx.ReadValue<Vector2>().x, ctx.ReadValue<Vector2>().y, 10));
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - jumpBoxDistance), jumpBoxSize);
    }
}
