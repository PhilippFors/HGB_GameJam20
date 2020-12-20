using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool alive;
    public float speed;
    public float gravity;

    [Header("Jump Settings")]
    public float fallmult;
    public float lowJump;
    public float jumpHeight;
    public bool isGrounded;

    [Header("Dash Settings")]
    public float dashDistance;
    public float drag;
    public float dashTime;
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
    Rigidbody rb => GetComponent<Rigidbody>();
    Vector3 move;
    Vector3 forward;
    bool isGrappling;
    private void Start()
    {
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

            // character.Move(move * Time.deltaTime);
            transform.position += move * Time.deltaTime;
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
        // character.Move(vel * Time.deltaTime);
        vel.x /= 1 + drag * Time.deltaTime;
        transform.position += vel * Time.deltaTime;
    }

    void Gravity()
    {
        if (!isDashing && !isGrappling)
            vel.y += gravity * Time.deltaTime;
        else
            vel.y = 0;

        isGrounded = Physics.CheckSphere(groundchecker.position, 0.2f, defaultMask, QueryTriggerInteraction.Ignore);
        if (isGrounded && vel.y < 0)
            vel.y = 0;
    }

    void Jump()
    {
        rb.velocity = Vector3.zero;
        vel += Vector3.up * Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void Dash()
    {

        if (inputManager.inputControls.Gameplay.Dash.triggered)
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
        rb.velocity = Vector3.zero;

    }

    void GrappleTo()
    {
        Debug.Log("grapple");
        if (grapple.target != null & grapple.canGrapple)
        {
            StartCoroutine(GrappleTime(grapple.target));
            // GetComponent<Rigidbody>().AddForce((grapple.target.position - transform.position) * grappleDistance, ForceMode.Impulse);
        }
    }

    IEnumerator GrappleTime(Transform t)
    {
        Transform target = t;
        float currentTime = 0;
        isGrappling = true;
        while (currentTime <= grappleTime)
        {
            float sp = grapplespeed.Evaluate(currentTime);
            currentTime += Time.deltaTime;
            rb.velocity = (target.position - transform.position) * sp;
            yield return null;
        }
        isGrappling = false;
        rb.velocity = Vector3.zero;
    }
}
