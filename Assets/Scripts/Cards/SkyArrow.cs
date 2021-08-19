using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyArrow : Card {

    public int dmg;
    
    public override void Action() { 
        if(CheckIfParentIsEnemy()) {            
            Enemy enemy = this.GetComponentInParent<Enemy>();
            this.RemoveFromHand();
            enemy.TakeDamage(dmg);
            enemy.Stuned();
            Player.Instance().UseAP(costAP);
            
            Destroy(this.gameObject);
        }
        else {
            this.transform.SetParent(Player.Instance().handGO.transform);
        }
    }
}
