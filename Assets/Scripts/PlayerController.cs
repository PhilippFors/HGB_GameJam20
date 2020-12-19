using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float gravity;
    public float jumpHeight;
    [SerializeField] InputManager inputManager;
    [SerializeField] CharacterController charController;
    Vector3 velocity;
    private float vSpeed = 0;
    public bool isGrounded;
    Vector3 vel = Vector3.zero;
    public LayerMask defaultMask;
    public Transform groundchecker;

    private void Update()
    {
        Gravity();
        Vector3 move = transform.forward * inputManager.move * speed;
        charController.Move(move * Time.deltaTime);

        if (isGrounded && inputManager.inputControls.Gameplay.Jump.triggered)
        {
            vel.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        vel.y += gravity * Time.deltaTime;
        charController.Move(vel * Time.deltaTime);
    }

    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundchecker.position, 0.5f, defaultMask);
        if (isGrounded && vel.y < 0)
            vel.y = 0;
    }
}
