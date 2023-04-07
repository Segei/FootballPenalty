using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GameLogic
{
    public class GoalTrugger : MonoBehaviour
    {
        public UnityEvent OnGloal;
        [SerializeField] private List<Collider2D> targetDetect;


        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (targetDetect.Contains(collision))
            {
                OnGloal?.Invoke();
            }
        }
    }
}
