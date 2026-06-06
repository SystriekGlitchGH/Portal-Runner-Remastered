using System.Collections;
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
    public GameObject Upgrade1Particles;
    public GameObject Upgrade2Particles;

    [Header("Movement")]
    // moving
    public float acceleration;
    public float deceleration;
    // this top speed is changed to fit running or regular
    public float topSpeed;
    // this is the speed used to hold what the regualr top speed is
    private float topRegSpeed;
    public float maxSpeed;

    private float directionX;

    // running
    public float topRunSpeed;

    // jumping
    public Vector2 jumpBoxSize;
    public float jumpBoxDistance;
    public float jumpForce;
    public LayerMask groundLayer;

    // portal stuff
    private Vector2 mousePos;

    // time slow
    public float timeSlowScale;
    public float timeSlowDuration;
    public float timeSlowCooldown;
    private bool canTimeSlow = true;

    [Header("Upgrades")]
    public bool hasUpgrade1;
    public bool hasUpgrade2;

    private void Awake()
    {
        topRegSpeed = topSpeed;
    }
    private void FixedUpdate()
    {
        Vector2 movementVelocity = new Vector2(directionX * topSpeed, 0);
        float currentSpeedRate = (movementVelocity.magnitude > 0) ? acceleration : deceleration;
        rb2d.linearVelocityX = Vector2.MoveTowards(rb2d.linearVelocity, movementVelocity, currentSpeedRate * Time.fixedDeltaTime).x;
        if(rb2d.linearVelocity.magnitude > maxSpeed)
        {
            rb2d.linearVelocity = Vector2.ClampMagnitude(rb2d.linearVelocity, maxSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Upgrade1"))
        {
            hasUpgrade1 = true;
            Instantiate(Upgrade1Particles,transform.position, Quaternion.Euler(Vector3.zero));
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Upgrade2"))
        {
            hasUpgrade2 = true;
            Instantiate(Upgrade2Particles, transform.position, Quaternion.Euler(Vector3.zero));
            Destroy(collision.gameObject);
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
    public void Run(InputAction.CallbackContext ctx)
    {
        if(ctx.ReadValue<float>() == 1)
        {
            topSpeed = topRunSpeed;
        }
        if(ctx.ReadValue<float>() == 0)
        {
            topSpeed = topRegSpeed;
        }
    }
    private bool isGrounded()
    {
        RaycastHit2D cast = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y - jumpBoxDistance), jumpBoxSize, 0, Vector2.up, 0, groundLayer);
        return cast;
    }
    public void PlacePortal1(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || !hasUpgrade1)
            return;

        var col = Physics2D.OverlapCircle(PortalPos, 0.2f, groundLayer);
        if (col)
            return;



        Portal[] portals = FindObjectsByType<Portal>(FindObjectsSortMode.None);
        for (int i = 0; i < portals.Length; i++)
        {
            if (portals[i].gameObject.CompareTag("Portal1"))
            {
                portals[i].innerParticles.Detach();
                portals[i].outerParticles.Detach();
                Destroy(portals[i].gameObject);
            }
        }
        Instantiate(portal1, PortalPos, Quaternion.Euler(Vector3.zero));
    }
    public void PlacePortal2(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || !hasUpgrade1)
            return;

        var col = Physics2D.OverlapCircle(PortalPos, 0.2f, groundLayer);
        if (col)
            return;



        Portal[] portals = FindObjectsByType<Portal>(FindObjectsSortMode.None);
        for (int i = 0; i < portals.Length; i++)
        {
            if (portals[i].gameObject.CompareTag("Portal2"))
            {
                portals[i].innerParticles.Detach();
                portals[i].outerParticles.Detach();
                Destroy(portals[i].gameObject);
            }
        }

        Instantiate(portal2, PortalPos, Quaternion.Euler(Vector3.zero));
    }
    private Vector2 PortalPos => Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
    public void MoveMouse(InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }
    public void TimeSlow(InputAction.CallbackContext ctx)
    {
        if (!canTimeSlow || !hasUpgrade2)
            return;
        if (ctx.performed)
        {
            StartCoroutine(SlowDownTime());
        }
    }
    private IEnumerator SlowDownTime()
    {
        canTimeSlow = false;
        Time.timeScale = timeSlowScale;
        Time.fixedDeltaTime = 0.02f * timeSlowScale;
        yield return new WaitForSecondsRealtime(timeSlowDuration);
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        yield return new WaitForSecondsRealtime(timeSlowCooldown);
        canTimeSlow = true;
    }

    public float getDirectionX()
    {
        return directionX;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - jumpBoxDistance), jumpBoxSize);
    }

}
