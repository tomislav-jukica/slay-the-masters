using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyArrow : Card {

    public int dmg;
    
    public override void Action() { 
        if(CheckIfParentIsEnemy()) {            
            Enemy enemy = this.GetComponentInParent<Enemy>();
            PlaySound();
            this.RemoveFromHand();
            enemy.TakeDamage(dmg);
            enemy.Stuned();
            Player.Instance().UseAP(costAP);            
            Banish();
        }
        else {
            this.transform.SetParent(Player.Instance().handGO.transform);
        }
    }
}
