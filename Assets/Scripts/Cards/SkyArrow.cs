using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyArrow : Card {

    public int dmg;
    
    public override void Action() { 
        if(CheckIfParentIsEnemy()) {
            Enemy enemy = this.GetComponentInParent<Enemy>();
            enemy.TakeDamage(dmg);
            enemy.Stuned();
            Player.Instance().UseAP(costAP);
            this.RemoveFromHand();
            Destroy(this.gameObject);
        } 
        
    }
}
