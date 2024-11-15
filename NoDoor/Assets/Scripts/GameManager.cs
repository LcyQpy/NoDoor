using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int sceneIndex
    {
        get
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }
    private void Start()
    {

    }
    public void GameOver()
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
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    public void OnStartGameBtn()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    private void Update()
    {
        Debug.Log("SceneIndex:" + sceneIndex);
    }

    public void LoadLevelByBuoldIndex(int buildIndex)
    {
        SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single);
    }
}
