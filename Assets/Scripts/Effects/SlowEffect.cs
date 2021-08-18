using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowEffect : PlayerEffect
{
    public int slowAmount;
    //public Text number;



    private void Start() {
        type = EffectType.SLOW;
        
        GameObject nGO = new GameObject("EffectNumber");
        number = nGO.gameObject.AddComponent<Text>();
        number.font = Player.Instance().battleLog.font;
        number.alignment = TextAnchor.LowerCenter;
        number.rectTransform.sizeDelta = new Vector2(100, 90);
        nGO.transform.SetParent(this.transform);
        number.text = slowAmount.ToString();

    }
    //public override void Activate() {
    //    Player player = Player.Instance();
    //    player.regenerationAP -= slowAmount;
    //    slowAmount -= 1;

    //    number.text = slowAmount.ToString();
    //    if (slowAmount == 0) {
    //        player.playerEffects.Remove(this);
    //        Destroy(this.gameObject.transform.parent.gameObject);
    //    }        
    //}


}
