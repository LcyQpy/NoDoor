using System.IO;
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

    public Animator animator;
    public Text level;
    private PlayerInfo playerInfo;
    private void Start()
    {
        if (sceneIndex > 1)
        {
            level = GameObject.Find("Level").GetComponent<Text>();
            level.text = SceneManager.GetActiveScene().name;
        }
        playerInfo = PlayerInfos.Instance.LoadGameData();
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
        // panel 面板播放结束动画
        animator.SetBool("gameOver", true);
    }
    public void LoadNextScene()
    {
        playerInfo.SetNowLevel(this.sceneIndex + 1);
        PlayerInfos.Instance.SaveGameData(playerInfo);
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
            SceneManager.LoadScene("GameStart", LoadSceneMode.Single);
        }
    }

    public void OnStartGameBtn()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void LoadLevelByBuoldIndex(int buildIndex)
    {
        SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single);
    }
}
