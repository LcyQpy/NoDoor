using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEditor.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PenalControl : MonoBehaviour
{
    public void OnLevelBtnClick(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
