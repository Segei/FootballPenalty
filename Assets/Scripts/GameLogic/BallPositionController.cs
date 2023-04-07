using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GameLogic
{
    public class BallPositionController : MonoBehaviour
    {
        [SerializeField] private Transform position;
        [SerializeField] private BallMover ballMover;
        [ShowNonSerializedField] private int countLife = 0;
        public UnityEvent BallStop;



        public void SetCountLife(int value) => countLife = value;

        private void Start()
        {
            ResetPosition();
        }

        public void AddForce(Vector2 direction)
        {
            if (ballMover.Velocity.magnitude > 0.1f || countLife == 0)
            {
                return;
            }
            ballMover.Hit(direction);
        }

        public void ResetPosition()
        {
            ballMover.Move(position.position);
        }

        private void Update()
        {
            if (ballMover.Velocity.magnitude > 0.1f || ballMover.gameObject.transform.position == position.position || countLife == 0)
            {
                return;
            }
            ResetPosition();
            BallStop.Invoke();
        }
    }
}