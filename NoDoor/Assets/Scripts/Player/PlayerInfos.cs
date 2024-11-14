using UnityEngine;
using Unity.IO;
using System.IO;
using UnityEngine.Assertions.Must;

public class PlayerInfos
{
    public PlayerInfo playerInfo = new PlayerInfo();

    private void DataInit()
    {
        playerInfo.hideKey = 0;
        playerInfo.nowLevel = 0;
    }

    public void SaveGameData()
    {
        if (playerInfo == null)
        {
            DataInit();
        }
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
            }
            playerInfo = JsonUtility.FromJson<PlayerInfo>(json);
        }
        else
        {
            DataInit();
        }
    }

    public void SetPlayerInfo(int index)
    {
        if (index > playerInfo.nowLevel)
        {
            playerInfo.nowLevel = index;
        }
    }
}

public class PlayerInfo
{
    public int hideKey;    
    public int nowLevel;
}
