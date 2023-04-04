using Assets.Scripts.Tools;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    public class Goalkeeper : MonoBehaviour
    {
        [SerializeField] private Transform goalkeeperTrasform, ball;
        [SerializeField] private Vector2 offsetPosition, start, end;
        [SerializeField] private bool onDrawGizmos;
        [SerializeField] private float radiusGizmo;
        [SerializeField, Range(0, 1)] private float lerp;
        private Vector2 startGlobal => start + (Vector2)transform.position;
        private Vector2 endGlobal => end + (Vector2)transform.position;




        private void OnDrawGizmos()
        {
            if (!onDrawGizmos)
            {
                return;
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(startGlobal, radiusGizmo);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(endGlobal, radiusGizmo);
        }

        private void Update()
        {

            Vector2 position = GetPoint(ball.position);
            position = position.x < startGlobal.x ? startGlobal : position.x > endGlobal.x ? endGlobal : position;
            goalkeeperTrasform.position = Vector2.Lerp(transform.position, position, lerp);
        }

        private Vector2 GetPoint(Vector2 position)
        {
            Vector2 leng = endGlobal - startGlobal;
            Vector2 project = position - startGlobal;
            Vector2 result = startGlobal + project.Project(leng.normalized);
            result -= offsetPosition;
            return result;
        }
    }
}
