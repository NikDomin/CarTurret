using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public interface IInputService
    {
        event Action<Vector2> OnScreenPosition;
    }

    public class InputHandler : MonoBehaviour, IInputService
    {
        public event Action<Vector2> OnScreenPosition;
        private PlayerInput playerInput;
        private Camera camera1;

        private InputAction moveAction;
        private void Awake()
        {
            camera1 = Camera.main;
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions.FindAction("Move");
        }

        private void Update()
        {
            OnScreenPosition?.Invoke(moveAction.ReadValue<Vector2>());
            Debug.Log("Mouse input x: " + GetPointerInput().x + "Mouse input y: " + GetPointerInput().y);
        }
     
        
        private Vector2 GetPointerInput()
        {
            Vector3 mousePosition = moveAction.ReadValue<Vector2>(); // Get the value from the mouse
            mousePosition.z = camera1.nearClipPlane;
            return camera1.ScreenToWorldPoint(mousePosition);
        }
    }
}