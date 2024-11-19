using TMPro;
using UnityEditor;
using UnityEditor.VersionControl;
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

    public Text level;
    public Animator animator;
    private void Start()
    {
        if (sceneIndex > 2)
        {
            level = GameObject.Find("Level").GetComponent<Text>();
            level.text = SceneManager.GetActiveScene().name;
        }
        
    }
    public int sceneIndex
    {
        get
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }

    public void GameOver()
    {
        animator = GameObject.Find("Finish").GetComponent<Animator>();
        animator.SetBool("gameOver", true);
        LoadNextScene();
    }
    public void LoadNextScene()
    {
        PlayerInfos.Instance.playerInfo.SetNowLevel(sceneIndex + 1);
        SceneManager.LoadScene(this.sceneIndex + 1, LoadSceneMode.Single);
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void OnBackBtnClick()
    {
        if (sceneIndex == 0)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }

    public void OnStartGameBtn()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void LoadLevelByBuoldIndex(int buildIndex)
    {
        SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single);
    }
}
