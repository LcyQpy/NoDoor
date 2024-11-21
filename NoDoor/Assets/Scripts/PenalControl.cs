using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEditor.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class PenalControl : MonoBehaviour
{
    [SerializeField]
    private int Levelnum;
    private bool isLock = true;
    private PlayerInfo playerInfo = new PlayerInfo();
    private void OnEnable()
    {
        playerInfo = PlayerInfos.Instance.LoadGameData();
    }
    private void Start()
    {
        if (Levelnum <= playerInfo.nowLevel)
        {
            this.GetComponent<Image>().color = new Color(1, 0.46f, 0);
            isLock = false;
        }
    }
    public void OnLevelBtnClick()
    {
        if (!isLock)
        {
            SceneManager.LoadScene(this.gameObject.name, LoadSceneMode.Single);
        }
    }

}
