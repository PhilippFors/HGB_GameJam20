using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Input inputControls;
    public Vector2 mousePos;
    public float move;
    private void Awake()
    {
        inputControls = new Input();
    }
    private void Start()
    {
        inputControls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<float>();
    }
    private void OnEnable()
    {
        inputControls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        inputControls.Gameplay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = inputControls.Gameplay.Mouse.ReadValue<Vector2>();
    }
}
