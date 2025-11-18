using UnityEngine;
using System.Collections.Generic;

public partial class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<Sound> sounds = new List<Sound>();
    private Dictionary<string, Sound> soundDict;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        soundDict = new Dictionary<string, Sound>();

        foreach (var s in sounds)
        {
            soundDict.Add(s.name, s);
        }
    }

    public void Play(string soundName)
    {
            if (!soundDict.TryGetValue(soundName, out Sound s))
            {
                Debug.LogWarning($"AudioManager: sound '{soundName}' not found.");
                return;
            }

            audioSource.clip = s.clip;
            audioSource.volume = s.volume;
            audioSource.pitch = s.pitch;
            audioSource.loop = s.loop;
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
