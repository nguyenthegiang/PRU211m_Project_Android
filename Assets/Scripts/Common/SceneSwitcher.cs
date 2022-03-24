using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
     public static int _lastSceneIndex;

    public void loadSceneByIndex(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void loadSceneByName(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    //Different function for Load Scene 3 from [Select Level] to avoid bug
    public void loadScene3()
    {
        removeSavedPosition();

        SceneManager.LoadSceneAsync("Scene3");
    }

    public static void goToGameOverScene() {
        //store the index of last scene
        _lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //load scene gameover
        SceneManager.LoadSceneAsync("GameOver");
    }

    public void restartLastScene() {
        removeSavedPosition();

        SceneManager.LoadSceneAsync(_lastSceneIndex);
    }

    //this method is called when user click the continue button in Game Menu
    public void ContinueButtonClick()
    {
        try
        {
            //load file
            JsonHandler handler = gameObject.AddComponent<JsonHandler>();
            handler.Load();
            //if there's no data -> go to Scene 1
            if (handler.data.sceneName == "")
            {
                throw new Exception();
            }
            //go to scene
            loadSceneByName(handler.data.sceneName);
        } catch (Exception ex)
        {
            //if can't find file -> go to Scene 1 (default)
            loadSceneByName("Scene1");
        }
    }

    //this method is called when user click the Start button in Game Menu
    public void StartButtonClick()
    {
        removeSavedPosition();

        //start from beginning
        loadSceneByName("Scene1");
    }

    //Delete file that store saved position to refresh
    public void removeSavedPosition()
    {
        JsonHandler handler = gameObject.AddComponent<JsonHandler>();
        handler.data = new SavedPositionData();
        handler.Save();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
