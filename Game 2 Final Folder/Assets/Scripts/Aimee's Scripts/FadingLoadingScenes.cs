using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadingLoadingScenes : MonoBehaviour
{
    // This is the basic image where additional images will be added on top depending on win/lose for the day.
    // Neutral image.
    public Image mainLoadingScene;

    // Arrays containing different images that will be added on top of the main image.
    // Bad and good images.
    public Image[] badDays = new Image[5];
    public Image[] goodDays = new Image[5];

    // Lists are arrays with no defined length. 
    // Making a list to store the bad & good images already enabled/used.
    public static List<int> usedBads = new List<int>();
    public static List<int> goodUsed = new List<int>();

    // Declare a local instance of the Save script to pull values/functions from.
    Save loadingScene;

    // Loads the level specified.
    public string loadLevel;

    // Defines the number of days set for the game. Change if more days are added.
    // numDays and the array length of badDays & goodDays need to match.
    public int numDays = 5;

    IEnumerator Start()
    {
        // Initialize the local instance of the Save script.
        loadingScene = new Save();

        // Makes all images disappear to start off with.
        SetAlphaZero();
 
        // Checks which layers get enabled, then adds and stores it in the appropriate list. 
        CheckLayers();

        // Gradually turns the alpha of the enabled images to full opacity. 
        FadeIn();

        // Loading screen transitions.
        yield return new WaitForSeconds(2.5f);

        // Gradually fades out the alpha of the enabled images to zero.
        FadeOut();
   
        yield return new WaitForSeconds(2.5f);

        // Loads the next level.
        SceneManager.LoadScene("TestScene 1");
    }

    private void Update()
    {
        // usedBads and goodUsed lists are constantly being set to the data previously stored and saved in the Save script.
        // This means that when the user saves and loads data the images already enabled for the room will stay enabled when they load the game.
        usedBads = loadingScene.loadBadDays();
        goodUsed = loadingScene.loadGoodDays();
    }

    // Orignally sets all of the images attached to the Canvas to alpha zero meaning it starts out transparent.
    public void SetAlphaZero()
    {
        // Base image alpha gets set to zero.
        mainLoadingScene.canvasRenderer.SetAlpha(0.0f);

        // Bad day images alpha gets set to zero.
        print("Bad days array length: " + badDays.Length);
        for (int i = 0; i < badDays.Length; i++)
        {
            badDays[i].canvasRenderer.SetAlpha(0.0f);
        }

        // Good day images alpha gets set to zero.
        print("Good days array length: " + goodDays.Length);
        for (int i = 0; i < goodDays.Length; i++)
        {
            goodDays[i].canvasRenderer.SetAlpha(0.0f);
        }
    }

    public void CheckLayers()
    {
        // Win condition is pulled from the script associated with a different scene.
        int Winning = PlayerPrefs.GetInt("Winning");
        print("Win (T or F): " + Winning);

        int random = Random.Range(0, numDays);
        print("Random number selected: " + random);

        if (Winning == 1)
        {
            while (FadingLoadingScenes.goodUsed.Contains(random))
            {
                random = Random.Range(0, numDays);
                print("New random number: " + random);
            }

            FadingLoadingScenes.goodUsed.Add(random);
            print("Good day added to goodUsed list: " + random);
        }

        if (Winning == 0)
        {
            while (FadingLoadingScenes.usedBads.Contains(random))
            {
                random = Random.Range(0, numDays);
                print("New random number: " + random);
            }

            FadingLoadingScenes.usedBads.Add(random);
            print("Bad day added to usedBads list: " + random);
        }

        // prints the list of good and bad days used in each list.
        foreach(int i in FadingLoadingScenes.goodUsed)
        {
            print("Good days: " + i);
        }

        foreach (int i in FadingLoadingScenes.usedBads)
        {
            print("Bad days: " + i);
        }

        // Adds the goodUsed and usedBads lists (where we just enabled which layer images will be turned on)
        // into the updateDays function of the Save script. This passes the data we just enabled and saves it.
        loadingScene.updateDays(goodUsed, usedBads);
    }

    void FadeIn()
    {
        mainLoadingScene.CrossFadeAlpha(1.0f, 1.5f, false);

        // Images that were enabled and stored in the list (using the checkLayersFix() method) have their Alpha returned to 1.
        // This means that the images will start to appear.
        foreach (int i in FadingLoadingScenes.goodUsed)
        {
            print("Turning on good days layer: " + i);

            goodDays[i].enabled = true;
            goodDays[i].CrossFadeAlpha(1, 1.5f, false);
        }
        foreach (int i in FadingLoadingScenes.usedBads)
        {
            print("Turning on bad days layer: " + i);

            badDays[i].enabled = true;
            badDays[i].CrossFadeAlpha(1, 1.5f, false);
        }
    }

    void FadeOut()
    {
        mainLoadingScene.CrossFadeAlpha(0.0f, 2.5f, false);

        foreach (int i in FadingLoadingScenes.goodUsed)
        {
            print("Turning off good days layer: " + i);
            goodDays[i].CrossFadeAlpha(0.0f, 2.5f, false);
        }

        foreach (int i in FadingLoadingScenes.usedBads)
        {
            print("Turning off bad days layer: " + i);
            badDays[i].CrossFadeAlpha(0.0f, 1.5f, false);
        }
    }
}
