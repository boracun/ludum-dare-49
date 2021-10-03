using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class ReputationManager : MonoBehaviour
    {
        public float reputation = 100f;
        public float reputationLoseLimit = 30f;

        private void Update()
        {
            if (reputation < reputationLoseLimit)
            {
                SceneManager.LoadScene("Ending");
            }
        }
    }
}
