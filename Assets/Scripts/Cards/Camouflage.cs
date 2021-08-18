using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camouflage : Card {
    [SerializeField]
    int turns;

    public override void Action() { 
        if(CheckIfParentIsPlayer()) {
            Player.Instance().Hide(turns);            
            Player.Instance().UseAP(costAP);
            this.RemoveFromHand();
            this.Banish();
        } 
        
    }
}
