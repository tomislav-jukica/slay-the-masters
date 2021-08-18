using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleArrow : Card {

    public int dmg;
    
    public override void Action() { 
        if(CheckIfParentIsEnemy()) {
            Enemy enemy = this.GetComponentInParent<Enemy>();
            enemy.TakeDamage(dmg);
            Player.Instance().UseAP(costAP);
            this.RemoveFromHand();
        } 
        
    }
}
