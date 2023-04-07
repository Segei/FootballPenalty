using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GameLogic
{
    public class LifeControll : MonoBehaviour
    {

        [SerializeField] private int maxLife;
        [ShowNonSerializedField] private int countLife;
        public UnityEvent<int> OnChangeLife;
        public UnityEvent OnEndGame;
        private void Start()
        {
            countLife = maxLife;
            OnChangeLife?.Invoke(countLife);
        }

        public void Hit()
        {
            if(countLife == 0)
            {
                return;
            }
            countLife--;
            OnChangeLife?.Invoke(countLife);
            if(countLife == 0)
            {
                OnEndGame?.Invoke();
            }
        }
    }
}
