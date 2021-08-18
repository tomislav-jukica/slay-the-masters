using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerEffect : MonoBehaviour
{
    public EffectType type;
    public Text number;
    //public abstract void Activate();

    public enum EffectType {
        SLOW,
        HIDE
    }
}
