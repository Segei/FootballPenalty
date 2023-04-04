using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class ViewLife : MonoBehaviour
    {
        [SerializeField] private List<GameObject> lifeView = new();


        private void OnValidate()
        {
            Clear();
        }


        public void SetCurrentLife(int value)
        {
            Clear();
            int result = value < lifeView.Count ? value : lifeView.Count;
            for(int i = 0; i < result; i++)
            {
                lifeView[i].SetActive(true);
            }

        }

        private void Clear()
        {
            foreach (GameObject go in lifeView)
            {
                go.SetActive(false);
            }
        }
    }
}
