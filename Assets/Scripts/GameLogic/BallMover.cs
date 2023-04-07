using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BallMover : MonoBehaviour
    {
        private Rigidbody2D rb;
        [SerializeField] private float power;

        public Vector2 Velocity => rb.velocity;



        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 position)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            transform.position = position;
        }

        public void Hit(Vector2 direction)
        {
            rb.AddForce(direction.normalized * power);
            rb.angularVelocity = Vector2.SignedAngle(Vector2.up, direction) * 20;
        }
    }
}
