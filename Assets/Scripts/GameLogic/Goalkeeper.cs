using System.Collections;
using Assets.Scripts.Tools;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.GameLogic
{
    public class Goalkeeper : MonoBehaviour
    {
        [SerializeField] private Transform goalkeeperTrasform, ball;
        [SerializeField] private float offsetPosition;
        [SerializeField] private bool onDrawGizmos;
        [SerializeField] private float radiusGizmo, speedReaction;
        private Vector2 startGlobal => (Vector2)transform.position - new Vector2(offsetPosition, 0);
        private Vector2 endGlobal => new Vector2(offsetPosition, 0) + (Vector2)transform.position;




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

        private IEnumerator InterceptBall()
        {
            yield return new WaitForSecondsRealtime(speedReaction);

            Vector2 projection = GetPoint(ball.position);
            Vector2 direction = projection - (Vector2)goalkeeperTrasform.position;
            Vector2 position =  direction.normalized * Random.Range(0, offsetPosition);
            position += (Vector2)transform.position;
            position = position.x < startGlobal.x ? startGlobal : position.x > endGlobal.x ? endGlobal : position;
            goalkeeperTrasform.position = position;
            yield return null;
        }

        private Vector2 GetPoint(Vector2 position)
        {
            Vector2 leng = endGlobal - startGlobal;
            Vector2 project = position - startGlobal;
            Vector2 result = startGlobal + project.Project(leng.normalized);
            return result;
        }

        public void MissedBal()
        {
            speedReaction /= 2;
            ResetGoalKeeper();
        }

        public void FootballerHitBall()
        {
            StartCoroutine(InterceptBall());
        }

        public void ResetGoalKeeper()
        {
            goalkeeperTrasform.position = transform.position;
        }

    }
}
