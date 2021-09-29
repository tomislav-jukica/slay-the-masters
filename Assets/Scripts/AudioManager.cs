using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    public List<Sound> sounds;


    public static AudioManager _instance;
    public AudioMixer mixer;
    private float musicVolume, sfxVolume = 1f;
    public static AudioManager Instance() { return _instance; }

    private Sound currentlyPlaying;

    public Text testText;
    private string test;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }
    private void Start() {
        musicVolume = 1;
    }
    private void Update() {
        
        testText.text = test;
    }

    public void SetLevelMusic(float sliderValue) {
        Debug.Log("music: " + sliderValue);
        Debug.Log(test);
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        //mixer.GetFloat("MusicVolume", out musicVolume);
        musicVolume = sliderValue;
        test = sliderValue.ToString();
        Debug.Log(test);
    }
    public void SetLevelSfx(float sliderValue) {
        mixer.SetFloat("SfxVolume", Mathf.Log10(sliderValue) * 20);
        sfxVolume = sliderValue;
    }
    public float GetMusicVolume() {
        return musicVolume;
    }
    public float GetSfxVolume() {
        return sfxVolume;
    }


    public void Play(string name, bool fromBegining = false) {
        bool found = false;
        foreach (Sound s in sounds) {            
            if(s.name == name) {
                if (currentlyPlaying != null) {
                    if (currentlyPlaying.name == name && fromBegining) { //isto ime i ispocetka
                        currentlyPlaying.source.Stop();
                        s.source.Play();
                        currentlyPlaying = s;
                        found = true;
                    }
                    else if (currentlyPlaying != null && currentlyPlaying.name != name) { //drugo ime
                        currentlyPlaying.source.Stop();
                        s.source.Play();
                        currentlyPlaying = s;
                        found = true;
                    } else {
                        found = true;
                    }
                }
                else {
                    s.source.Play();
                    currentlyPlaying = s;
                    found = true;
                }                
            } 
        }
        if(!found) Debug.LogError("Sound: " + name + " not found!");
    }
}
