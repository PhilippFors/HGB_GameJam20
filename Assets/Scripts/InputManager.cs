using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Input inputControls;
    public Vector2 mousePos;
    public float move;
    public bool isMoving = false;
    private void Awake()
    {
        inputControls = new Input();
    }
    private void Start()
    {
        inputControls.Gameplay.Move.performed += ctx => Move(ctx.ReadValue<float>());
        inputControls.Gameplay.Move.canceled += ctx => NoMove();
    }
    private void OnEnable()
    {
        inputControls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        inputControls.Gameplay.Disable();
    }

    void Move(float m)
    {
        move = m;
        isMoving = true;
    }
    void NoMove()
    {
        move = 0;
        isMoving = false;
    }

    void Update()
    {
        mousePos = inputControls.Gameplay.Mouse.ReadValue<Vector2>();
    }
}
