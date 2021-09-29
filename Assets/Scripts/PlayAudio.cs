using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public string audioName = "";
    private void Awake() {
        AudioManager.Instance().Play(audioName);
    }
}
