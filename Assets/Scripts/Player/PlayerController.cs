using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool alive;
    public float speed;
    public float gravity;
    public float groundcheckerRadius;

    [Header("Jump Settings")]
    public float fallmult;
    public float lowJump;
    public float jumpHeight;
    public bool isGrounded;

    [Header("Dash Settings")]
    public float dashDistance;
    public float drag;
    public float dashTime;
    public float dashCooldown;
    bool isDashing = false;

    [Header("Grapple settings")]
    public float grappleTime = 0.2f;
    public AnimationCurve grapplespeed;
    [SerializeField] InputManager inputManager;
    public CharacterController character;
    public Grapple grapple;
    Vector3 vel = Vector3.zero;
    public LayerMask defaultMask;
    public Transform groundchecker;
    Vector3 currentMoveDirection;
    Rigidbody rb;
    Vector3 move;
    Vector3 forward;
    bool isGrappling;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        forward = Camera.main.transform.right;
        inputManager.inputControls.Gameplay.Grapple.performed += ctx => GrappleTo();
        inputManager.inputControls.Gameplay.Dash.performed += ctx => Dash();
        inputManager.inputControls.Gameplay.Jump.performed += ctx => Jump();
    }

    private void Update()
    {
        if (!alive)
            return;

        Gravity();

        if (!isDashing)
        {
            currentMoveDirection = forward * inputManager.move;
            move = currentMoveDirection * speed;

            character.Move(move * Time.deltaTime);
            // transform.position += move * Time.deltaTime;
        }

        if (inputManager.move != 0)
            transform.rotation = Quaternion.LookRotation(currentMoveDirection, Vector3.up);

        if (!isGrappling && !isDashing)
        {
            if (vel.y < 0)
                vel += Vector3.up * gravity * (fallmult - 1) * Time.deltaTime;
            else if (vel.y > 0 && !UnityEngine.Input.GetKey(KeyCode.Space))
                vel += Vector3.up * gravity * (lowJump - 1) * Time.deltaTime;
        }

        vel.x /= 1 + drag * Time.deltaTime;
        character.Move(vel * Time.deltaTime);
        // transform.position += vel * Time.deltaTime;
    }

    void Gravity()
    {
        if (!isDashing && !isGrappling)
            vel.y += gravity * Time.deltaTime;
        else
            vel.y = 0;

        isGrounded = Physics.CheckSphere(groundchecker.position, groundcheckerRadius, defaultMask, QueryTriggerInteraction.Ignore);
        if (isGrounded && vel.y < 0)
            vel.y = 0;
    }

    void Jump()
    {
        if (isGrounded)
        {
            vel += Vector3.up * Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Dash()
    {
        if (canDash)
        {
            StartCoroutine(DashCountdown());
            vel += Vector3.Scale(transform.forward,
                                       dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * drag + 1)) / -Time.deltaTime),
                                                                  0,
                                                                  (Mathf.Log(1f / (Time.deltaTime * drag + 1)) / -Time.deltaTime)));
            vel.y = 0;
        }
    }

    IEnumerator DashCountdown()
    {
        isDashing = true;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        StartCoroutine(DashCooldown());
        rb.velocity = Vector3.zero;

    }
    bool canDash = true;
    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void GrappleTo()
    {
        Debug.Log("grapple");
        if (grapple.target != null & grapple.canGrapple)
        {
            StartCoroutine(GrappleTime(grapple.target));
        }
    }

    IEnumerator GrappleTime(Transform t)
    {
        character.enabled = false;
        rb.isKinematic = false;
        isGrappling = true;
        Transform target = t;
        Vector3 dir = target.position - transform.position;
        float currentTime = 0;

        while (currentTime <= grappleTime)
        {
            float sp = grapplespeed.Evaluate(currentTime);
            currentTime += Time.deltaTime;
            rb.velocity = (target.position - transform.position) * sp;
            yield return null;
        }

        isGrappling = false;
        rb.isKinematic = true;
        character.enabled = true;
        rb.velocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundchecker.position, groundcheckerRadius);
    }
}
