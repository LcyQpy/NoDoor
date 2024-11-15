using UnityEngine;
using System.IO;
using System;
using UnityEditor.Playables;

public class PlayerInfos : MonoBehaviour
{
    private static PlayerInfos instance;
    public static PlayerInfos Instance
    {
        get
        {
            if (instance == null)
            { 
                instance = new PlayerInfos();
            }
            return instance;
        }
    }

    public PlayerInfo playerInfo = new PlayerInfo();
    private void Start()
    {
        LoadGameData();
    }

    private void InitPlayerInfo()
    {
        playerInfo = new PlayerInfo();
    }
    public void SaveGameData( )
    {
        string json = JsonUtility.ToJson(playerInfo);
        string filePath = Application.streamingAssetsPath + "/PlayerData.json";
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine(json);
            sw.Close();
            sw.Dispose();
        }
    }

    public void LoadGameData()
    {
        string json;
        string filePath = Application.streamingAssetsPath + "/PlayerData.json";
        if (File.Exists(filePath))
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                json = sr.ReadToEnd();
                sr.Close();
                playerInfo = JsonUtility.FromJson<PlayerInfo>(json);
            }
        }
        else
        {
            InitPlayerInfo();
        }
    }
    private void OnDestroy()
    {
        SaveGameData();
        playerInfo.SetTotalTime(Time.realtimeSinceStartup);
    }
}

public class PlayerInfo
{
    public int hideKey = 0;    
    public int nowLevel = 0;
    public float totalTime = 0f;
    public void SetHideKey()
    {
        hideKey++;
    }
    public void SetNowLevel(int index)
    {
        if (index > nowLevel)
        {
            nowLevel = index;
        }
    }

    public void SetTotalTime(float time)
    {
        totalTime += time;
    }
}
