using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    private static AudioSourceManager instance;
    public static AudioSourceManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AudioSourceManager();
            }
            return instance;
        }
    }
}

