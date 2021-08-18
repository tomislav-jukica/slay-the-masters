using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBag : Card {
    [SerializeField]
    int apRestore = 2;
    public override void Action() { 
        if(CheckIfParentIsEnemy()) {
            Player.Instance().currentAP += apRestore;
            Player.Instance().UseAP(costAP);
            this.RemoveFromHand();
            this.Banish();
        } 
        
    }
}
