using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInit : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
        // 热更
        // 初始化UI
        SceneManager.LoadScene("GameStart", LoadSceneMode.Single);
    }


    [RuntimeInitializeOnLoadMethod]
    private static void OnLoadStartGame()
    {
        if (SceneManager.GetActiveScene().name != "GameStart")
        {
            SceneManager.LoadScene("GameStart", LoadSceneMode.Single);
        }
    }
}
