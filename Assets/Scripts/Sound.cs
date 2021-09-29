using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound 
{
    [HideInInspector]
    public AudioSource source;

    public AudioMixerGroup mixerGroup;

    public string name;
    public AudioClip clip;
    public bool loop = true;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(.1f, 3f)]
    public float pitch = 1f;
}
