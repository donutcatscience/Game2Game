using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Save : MonoBehaviour {
    public GameObject pauseCanvas;
    public bool pausetoggle = false;

    public int gameDayNum = 0;
    public int resourceCount = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    
    }
    public void saveIt()
    {
        Debug.Log("Save");
        Debug.Log(gameDayNum);
        Debug.Log(resourceCount);
        gameDayNum++;
        resourceCount++;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.OpenOrCreate);

        gameData game = new gameData();
        game.gameDay = gameDayNum;
        game.resourceCounter = resourceCount;
        bf.Serialize(file, game);
        file.Close();
    }

    public void loadIt()
    {
        Debug.Log("Load");
        if(File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            gameDayNum++;
            resourceCount++;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
            gameData game = (gameData)bf.Deserialize(file);
            file.Close();

            gameDayNum = game.gameDay;
            resourceCount = game.resourceCounter;
        }
    }

    [Serializable]
    class gameData
    {
        public int gameDay;
        public int resourceCounter;
    }
}
