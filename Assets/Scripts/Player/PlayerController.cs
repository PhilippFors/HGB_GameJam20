using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    bool isDashing;
    [SerializeField] InputManager inputManager;
    public CharacterController character;

    Vector3 vel = Vector3.zero;
    public LayerMask defaultMask;
    public Transform groundchecker;
    Vector3 currentMoveDirection;
    Rigidbody rb => GetComponent<Rigidbody>();

    Vector3 forward;
    private void Start()
    {
        forward = Camera.main.transform.right;
    }

    private void Update()
    {
        Gravity();
        currentMoveDirection = forward * inputManager.move;
        Vector3 move = currentMoveDirection * speed;
        character.Move(move * Time.deltaTime);

        if (inputManager.move != 0)
            transform.rotation = Quaternion.LookRotation(currentMoveDirection, Vector3.up);


        Dash();
        Jump();

        if (vel.y < 0)
            vel += Vector3.up * gravity * (fallmult) * Time.deltaTime;
        else if (vel.y > 0 && !inputManager.inputControls.Gameplay.Jump.triggered)
            vel += Vector3.up * gravity * (lowJump) * Time.deltaTime;

        character.Move(vel * Time.deltaTime);
    }

    void Gravity()
    {
        if (!isDashing)
            vel.y += gravity * Time.deltaTime;

        isGrounded = Physics.CheckSphere(groundchecker.position, 0.5f, defaultMask, QueryTriggerInteraction.Ignore);
        if (isGrounded && vel.y < 0)
            vel.y = 0;
    }

    void Jump()
    {
        if (isGrounded && inputManager.inputControls.Gameplay.Jump.triggered)
        {
            vel += Vector3.up * Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    void Dash()
    {
        if (inputManager.inputControls.Gameplay.Dash.triggered)
        {
            StartCoroutine(DashCountdown());
            vel = Vector3.Scale(transform.forward,
                                       dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * drag + 1)) / -Time.deltaTime),
                                                                  0,
                                                                  (Mathf.Log(1f / (Time.deltaTime * drag + 1)) / -Time.deltaTime)));
        }

        vel.x /= 1 + drag * Time.deltaTime;
        // vel.y /= 1 + drag * Time.deltaTime;
        // vel.z /= 1 + drag * Time.deltaTime;
    }

    IEnumerator DashCountdown()
    {
        isDashing = true;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
    }
}
