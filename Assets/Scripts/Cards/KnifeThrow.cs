using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrow : Card {

    public int dmg;
    
    public override void Action() { 
        if(CheckIfParentIsEnemy()) {            
            Enemy enemy = this.GetComponentInParent<Enemy>();
            this.RemoveFromHand();
            enemy.TakeDamage(dmg);
            Player.Instance().UseAP(costAP);
            
            this.Banish();
            
        } 
        
    }
}
