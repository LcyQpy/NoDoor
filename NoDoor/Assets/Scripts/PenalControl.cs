using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEditor.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PenalControl : MonoBehaviour
{
    [SerializeField]
    private int Levelnum;
    public void OnLevelBtnClick()
    {
        SceneManager.LoadScene(this.gameObject.name, LoadSceneMode.Single);
    }

}
