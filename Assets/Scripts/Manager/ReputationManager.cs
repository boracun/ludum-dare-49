using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
