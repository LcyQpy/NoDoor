using UnityEngine;
using UnityEngine.Events;

public class AudioSourceManager : MonoSingleton<AudioSourceManager>
{
    // Instance

    // Music
    private AudioSource musicAudioSource;
    private float musicVolume = 1.0f;

    // Sound
    private AudioSource[] soundAudioSources;
    private float soundVolume = 1.0f;
    private int soundSorceIndex;
    private void Start()
    {
        // test
        AudioSourceManager.Instance.PlayMusic("BGM");
        AudioSourceManager.Instance.ChangeMusicVolume(0.15f);
    }

    protected override void Awake()
    {
        base.Awake();

        // Music 
        GameObject newMusicAudioSource = new GameObject("Music Source");
        musicAudioSource = newMusicAudioSource.AddComponent<AudioSource>();
        musicAudioSource.transform.SetParent(transform);

        // Sound
        soundAudioSources = new AudioSource[7];
        for(int i = 0; i < soundAudioSources.Length; i++)
        {
            GameObject newSoundAudioSource = new GameObject($"Sound Source{i + 1}");
            soundAudioSources[i]= newSoundAudioSource.AddComponent<AudioSource>();
            newSoundAudioSource.transform.SetParent (transform);
        }
    }

    #region Music
    public void PlayMusic(string musicName)
    {
        musicAudioSource.clip = Resources.Load<AudioClip>("Audio/Music/" + musicName);
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void PauseMusic()
    {
        musicAudioSource.Pause();
    }

    public void UnPauseMusic()
    {
        musicAudioSource.UnPause();
    }

    public void StopMusic()
    {
        musicAudioSource.Stop();
    }

    public void ChangeMusicVolume(float volume)
    {
        musicVolume = volume;
        musicAudioSource.volume = volume;
    }

    #endregion

    #region Sound
    public void PlaySound(string soundName, bool isLoop = false, UnityAction<AudioSource> callback = null)
    {
        ResourceRequest req  = Resources.LoadAsync<AudioClip>(soundName);
        soundSorceIndex++;
        soundSorceIndex %= soundAudioSources.Length; 
        soundAudioSources[soundSorceIndex].clip = req.asset as AudioClip;
        soundAudioSources[soundSorceIndex].volume = soundVolume;
        soundAudioSources[soundSorceIndex].loop = isLoop;
        callback?.Invoke(soundAudioSources[soundSorceIndex]);
    }

    public void StopSound(string soundName)
    {
        foreach (var audioSource in soundAudioSources) 
        {
            if (audioSource.isPlaying && audioSource.clip.name == soundName) { }
            {
                audioSource.Stop();
            }
        }
    }

    public void StopAllSound()
    {
        foreach(var audioSource in soundAudioSources)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    public void ChangeSoundVolume(float volume)
    {
        soundVolume = volume;
        foreach(var soundAudio in soundAudioSources)
        {
            soundAudio.volume = soundVolume;
        }
    }
    #endregion

}

