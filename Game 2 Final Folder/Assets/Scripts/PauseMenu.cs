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
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pausetoggle)
            {
                pausetoggle = false;
            }
            else
            {
                pausetoggle = true;
            }

            Cursor.visible = true;
            pauseCanvas.SetActive(pausetoggle); 

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
