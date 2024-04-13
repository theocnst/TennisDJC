using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private AudioSource[] audioSources;
    private int sourceCount = 5;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
            LoadAudioClips();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudioSources()
    {
        audioSources = new AudioSource[sourceCount];
        for (int i = 0; i < sourceCount; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            audioSources[i] = source;
        }
    }

    private void LoadAudioClips()
    {
        // Load and assign audio clips to the dictionary
        audioClips["soft_hit"] = Resources.Load<AudioClip>("soft_hit");
        // Add more clips as needed
    }

    public void PlayClip(string clipName, float volume)
    {
        if (audioClips.TryGetValue(clipName, out AudioClip clip))
        {
            AudioSource availableSource = FindAvailableSource();
            if (availableSource != null)
            {
                availableSource.PlayOneShot(clip, volume);
            }
        }
    }

    private AudioSource FindAvailableSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
                return source;
        }
        return null;
    }
}
