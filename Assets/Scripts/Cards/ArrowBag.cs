using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBag : Card {
    [SerializeField]
    int apRestore = 2;
    public override void Action() { 
        if(CheckIfParentIsPlayer()) {
            Player.Instance().currentAP += apRestore;
            PlaySound();
            Player.Instance().UseAP(costAP);
            this.RemoveFromHand();
            this.Banish();
        }
        else {
            this.transform.SetParent(Player.Instance().handGO.transform);
        }
    }
}
