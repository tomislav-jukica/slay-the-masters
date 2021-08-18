using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerEffect : MonoBehaviour
{
    public EffectType type;
    public abstract void Activate();

    public enum EffectType {
        SLOW
    }
}
