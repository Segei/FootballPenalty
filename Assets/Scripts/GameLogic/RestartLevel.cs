using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameLogic
{
    public class RestartLevel : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(gameObject.scene.name);
        }
    }
}
