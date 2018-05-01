using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour {
    public GameObject pauseCanvas;
    public static bool pausetoggle;
    Save file; 
    // Use this for initialization
    void Start () {
        file = new Save();
        pauseCanvas.SetActive(false);
        pausetoggle = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pausetoggle = !pausetoggle;

            Cursor.visible = !Cursor.visible;
            pauseCanvas.SetActive(pausetoggle);

            if (pausetoggle)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }



            
        }

    }
    public void saveGame()
    {
        file.saveIt();
    }
    public void LoadGame()
    {
        file.loadIt();
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
