using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public static void goToGameOverScene() {
        //store the index of last scene
        _lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //load scene gameover
        SceneManager.LoadSceneAsync("GameOver");
    }

    public void restartLastScene() {
        //Delete file that store saved position to refresh
        JsonHandler handler = gameObject.AddComponent<JsonHandler>();
        handler.data = new SavedPositionData();
        handler.Save();

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
        //Delete file that store saved position to refresh
        JsonHandler handler = gameObject.AddComponent<JsonHandler>();
        handler.data = new SavedPositionData();
        handler.Save();

        //start from beginning
        loadSceneByName("Scene1");
    }
}
