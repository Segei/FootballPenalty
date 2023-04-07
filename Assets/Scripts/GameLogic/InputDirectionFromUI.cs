using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GameLogic
{
    public class InputDirectionFromUI : MonoBehaviour
    {
        private Vector2 startPosition, endPosition;
        public UnityEvent<Vector2> OnHitDirection;


        private void Awake()
        {
            InputActions inputs = new();
            inputs.Android.Touch.started += e => startPosition = inputs.Android.Position.ReadValue<Vector2>();
            inputs.Android.Position.performed += e =>
            {
                endPosition = e.ReadValue<Vector2>();
            };
            inputs.Android.Touch.canceled += e =>
            {
                EndTouch();
            };
            inputs.Enable();
        }


        private void EndTouch()
        {
            OnHitDirection?.Invoke(endPosition - startPosition);
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
        }
    }
}
