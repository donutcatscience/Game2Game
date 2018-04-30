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

    // Declares 2 lists for good and bad days.
    static List<int> Gdays;
    static List<int> Bdays;

    // Use this for initialization.
    void Start () {
        Gdays = new List<int>();
        Bdays = new List<int>();
    }
	
	// Retrieving the values of the lists from FadingLoadingScenes script and setting it to local variables within this script.
	public void updateDays(List<int> good, List<int> bad)
    {
        Gdays = good;
        Bdays = bad;
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

        // Saves bad and good days used/images enabled.
        game.usedBads = Bdays;
        game.goodUsed = Gdays;

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

            // Loading good and bad days used/images enabled.
            Gdays = game.goodUsed;
            Bdays = game.usedBads;
        }
    }

    // Returns the lists of good and bad days enabled.
    // In the FadingLoadingScenes script we'll be pulling from these methods to load data previously saved.
    public List<int> loadGoodDays()
    {
        return Gdays;
    }

    public List<int> loadBadDays()
    {
        return Bdays;
    }

    [Serializable]
    public class gameData
    {
        public int gameDay;
        public int resourceCounter;

        public List<int> usedBads = new List<int>();
        public List<int> goodUsed = new List<int>();
    }
}
