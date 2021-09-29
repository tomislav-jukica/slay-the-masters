using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDirt : Card {

    
    public override void Action() { 
        if(CheckIfParentIsEnemy()) {
            Enemy enemy = this.GetComponentInParent<Enemy>();
            PlaySound();
            enemy.Stuned();
            Player.Instance().UseAP(costAP);
            this.RemoveFromHand();
        }
        else {
            this.transform.SetParent(Player.Instance().handGO.transform);
        }

    }
}
