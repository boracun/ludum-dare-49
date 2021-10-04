using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class TimeManager : MonoBehaviour
    {
        public const int MAX_SECONDS = 420;
        public const int START_HOUR = 17;
        public float timeOfPlayInSeconds;
        public Text clockText;
        private float time = 0f;

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            clockText.text = GetHours() + ":" + GetMinutes();
            if (time.Equals(timeOfPlayInSeconds))
            {
                SceneManager.LoadScene("Ending");
            }
        }

        private string GetHours()
        {
            int passedMinutes = (int) (GetSecondCoefficient() * time / 60);
            passedMinutes += START_HOUR;
            if (passedMinutes < 10)
                return "0" + passedMinutes;
            return passedMinutes.ToString();
        }

        private string GetMinutes()
        {
            int passedSeconds = (int) (GetSecondCoefficient() * time % 60);
            if (passedSeconds < 10)
                return "0" + passedSeconds;
            return passedSeconds.ToString();
        }

        private float GetSecondCoefficient()
        {
            return MAX_SECONDS / timeOfPlayInSeconds;
        }
    }
}
