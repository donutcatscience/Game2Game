using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionBack : MonoBehaviour
{
    public string transitionBack;

	
	// Update is called once per frame
	void Update ()
    {
        if (ObjectiveTrigger.win != null)
        {
            if (ObjectiveTrigger.win == true)
            {
                PlayerPrefs.SetInt("Winning", 1);
            }

            else if (ObjectiveTrigger.win == false)
            {
                PlayerPrefs.SetInt("Winning", 0);
            }
            ObjectiveTrigger.win = null;
            SceneManager.LoadScene("LoadingScreens");
        }
	}

}
