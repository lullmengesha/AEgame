using UnityEngine;

public partial class AudioManager
{
    [System.Serializable]
    public class Sound
    {
        public string name;          // Clip name you will call
        public AudioClip clip;
        public float volume = 1f;
        public float pitch = 1f;
        public bool loop = false;
    }
}
