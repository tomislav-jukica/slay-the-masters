using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headshot : Card {

    public int dmg;
    
    public override void Action() { 
        if(CheckIfParentIsEnemy()) {            
            float rng = Random.Range(0f, 1f);
            if (rng >= 0.5f) {
                Enemy enemy = this.GetComponentInParent<Enemy>();
                this.RemoveFromHand();
                enemy.TakeDamage(dmg);
            } else {
                Player.Instance().battleLog.Write("Missed!", Color.red);
            }           
            Player.Instance().UseAP(costAP);
            
        } 
        
    }
}
