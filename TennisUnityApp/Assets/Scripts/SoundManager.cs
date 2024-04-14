using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private AudioSource[] audioSources;
    private int sourceCount = 7;

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
            source.loop = true; // Set loop to true for all sources
            audioSources[i] = source;
        }
    }

    private void LoadAudioClips()
    {
        audioClips["soft_hit"] = Resources.Load<AudioClip>("soft_hit");
        audioClips["hard_hit"] = Resources.Load<AudioClip>("hard_hit");
        audioClips["footsteps"] = Resources.Load<AudioClip>("footsteps");
        audioClips["ball_bounce"] = Resources.Load<AudioClip>("ball_bounce");
        audioClips["net_hit"] = Resources.Load<AudioClip>("net_hit");
        audioClips["background_music"] = Resources.Load<AudioClip>("background_music");
        audioClips["ambient_park"] = Resources.Load<AudioClip>("ambient_park");
    }
    public void PlayClip(string clipName)
    {
        if (audioClips.TryGetValue(clipName, out AudioClip clip))
        {
            AudioSource availableSource = FindAvailableSource();
            if (availableSource != null)
            {
                availableSource.PlayOneShot(clip);
            }
        }
    }

    public void PlayLoop(string clipName)
    {
        if (audioClips.TryGetValue(clipName, out AudioClip clip))
        {
            AudioSource availableSource = FindAvailableSource();
            if (availableSource != null && !availableSource.isPlaying)
            {
                availableSource.clip = clip;
                availableSource.Play();
            }
        }
    }

    public void StopLoop(string clipName)
    {
        if (audioClips.TryGetValue(clipName, out AudioClip clip))
        {
            foreach (AudioSource source in audioSources)
            {
                if (source.clip == clip && source.isPlaying)
                {
                    source.Stop();
                }
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
