using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour {
    public GameObject pauseCanvas;
    public bool pausetoggle = false;
    Save file; 
    // Use this for initialization
    void Start () {
        file = new Save();
        pauseCanvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pausetoggle = !pausetoggle;

            Cursor.visible = !Cursor.visible;
            pauseCanvas.SetActive(pausetoggle);

            if (pausetoggle) Time.timeScale = 0f;
            else Time.timeScale = 1f;


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
