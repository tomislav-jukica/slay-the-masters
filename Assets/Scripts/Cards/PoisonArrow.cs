using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArrow : Card {

    public int dmg;
    public int poison;
    
    public override void Action() { 
        if(CheckIfParentIsEnemy()) {            
            Enemy enemy = this.GetComponentInParent<Enemy>();
            this.RemoveFromHand();
            enemy.TakeDamage(dmg);
            enemy.Poisoned(poison);
            Player.Instance().UseAP(costAP);            
        }
        else {
            this.transform.SetParent(Player.Instance().handGO.transform);
        }
    }
}
