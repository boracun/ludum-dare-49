using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class ReputationManager : MonoBehaviour
    {
        public float reputation = 100f;
        public float reputationLoseLimit = 0f;
        public GameObject reputationBar;
        private GameObject reputationBarProgress;
        public Text reputationBarText;
        private float startScaleX;

        private void Start()
        {
            reputationBarProgress = reputationBar.transform.GetChild(2).gameObject;
            startScaleX = reputationBarProgress.transform.localScale.x;
        }

        private void Update()
        {
            UpdateReputationBar();
            if (reputation < reputationLoseLimit)
            {
                SceneManager.LoadScene("Ending");
            }
        }

        private void UpdateReputationBar()
        {
            reputationBarText.text = reputation + "%";
            var localScale = reputationBarProgress.transform.localScale;
            localScale = new Vector3(reputation * startScaleX / 100f,
                localScale.y, localScale.z);
            reputationBarProgress.transform.localScale = localScale;

            var transformPosition = reputationBarProgress.transform.localPosition;
            transformPosition.x = (startScaleX - localScale.x) / -2f;
            reputationBarProgress.transform.localPosition = transformPosition;
        }
    }
}
