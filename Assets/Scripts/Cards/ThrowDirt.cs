using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDirt : Card {

    
    public override void Action() { 
        if(CheckIfParentIsEnemy()) {
            Enemy enemy = this.GetComponentInParent<Enemy>();
            enemy.Stuned();
            Player.Instance().UseAP(costAP);
            this.RemoveFromHand();
        } 
        
    }
}
