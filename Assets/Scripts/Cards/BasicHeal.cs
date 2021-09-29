using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHeal : Card {

    public int healAmount;
    
    public override void Action() { 
        if(CheckIfParentIsPlayer()) {
            PlaySound();
            Player.Instance().Heal(healAmount);
            Player.Instance().UseAP(costAP);
            this.RemoveFromHand();
        } else if (CheckIfParentIsEnemy()) {
            Enemy enemy = this.GetComponentInParent<Enemy>();
            PlaySound();
            enemy.Heal(healAmount);
            Player.Instance().UseAP(costAP);
            this.RemoveFromHand();
        }
        else {
            this.transform.SetParent(Player.Instance().handGO.transform);
        }
    }
}
