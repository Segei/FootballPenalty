using UnityEngine;

namespace Assets.Scripts
{
    public class TestTruger : MonoBehaviour
    {
        [SerializeField] private TestAddForce test;


        public void OnTriggerEnter2D(Collider2D collision)
        {
            test.ResetPosition();
        }
    }
}
