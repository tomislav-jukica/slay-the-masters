using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleArrow : Card {

    public int dmg;
    
    public override void Action() { 
        if(CheckIfParentIsEnemy()) {            
            Enemy enemy = this.GetComponentInParent<Enemy>();
            PlaySound();
            this.RemoveFromHand();
            enemy.TakeDamage(dmg);
            Player.Instance().UseAP(costAP);            
        }
        else {
            this.transform.SetParent(Player.Instance().handGO.transform);
        }
    }
}
