using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();   
            }
            return instance;
        }
    }

    private int buildIndex;
    private void Start()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void GameOver()
    {
        SceneBuildIndex();
        SceneManager.LoadScene(buildIndex + 1, LoadSceneMode.Single);
        var js =  new PlayerInfos();
        js.LoadGameData();
        
        js.SaveGameData();
    }
    public void ReloadLevel()
    {
        SceneBuildIndex();
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }

    public void OnBackBtnClick()
    {
        if (buildIndex == 0)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    public void OnStartGameBtn()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void SceneBuildIndex()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
