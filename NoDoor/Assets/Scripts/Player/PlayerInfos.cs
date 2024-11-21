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

    private void Start()
    {
        LoadGameData();
    }

    public void SaveGameData(PlayerInfo playerInfo)
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

    public PlayerInfo LoadGameData()
    {
        PlayerInfo playerInfo;
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
            using (StreamWriter sw = File.CreateText(filePath))
            {
                playerInfo = new PlayerInfo();
                json = JsonUtility.ToJson(playerInfo);
                sw.WriteLine(json);
            }
        }
        return playerInfo;
    }
}

public class PlayerInfo
{
    public int hideKey = 0;    
    public int nowLevel = 1;
    private int MaxLevel = 15;
    public void SetHideKey()
    {
        hideKey++;
    }
    public void SetNowLevel(int index)
    {
        if (index <= MaxLevel)
        {
            if (index > nowLevel)
            {
                nowLevel = index;
            }
        }
        else
        {
            nowLevel = MaxLevel;
        }
    }
}
