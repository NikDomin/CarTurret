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
        private UnityEngine.Camera camera1;

        private InputAction moveAction;
        private void Awake()
        {
            camera1 = UnityEngine.Camera.main;
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions.FindAction("Move");
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            OnScreenPosition?.Invoke(moveAction.ReadValue<Vector2>());
        }
    }
}