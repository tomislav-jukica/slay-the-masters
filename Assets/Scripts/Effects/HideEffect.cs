using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideEffect : PlayerEffect
{
    public int turn;

    private void Start() {
        type = EffectType.HIDE;
        GameObject nGO = new GameObject("EffectNumber");
        number = nGO.gameObject.AddComponent<Text>();
        number.font = Player.Instance().battleLog.font;
        number.alignment = TextAnchor.LowerCenter;
        number.rectTransform.sizeDelta = new Vector2(100, 90);
        nGO.transform.SetParent(this.transform);
        number.text = turn.ToString();
    }
}
