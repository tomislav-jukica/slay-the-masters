using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainOfArrows : Card
{
    public int dmg;
    public override void Action() {
        if(CheckIfParentIsEnemy()) {
            
            for (int i = 0; i < BattleManager.Instance().liveEnemies.Count; i ++) {
                Enemy e = BattleManager.Instance().liveEnemies[i];
                this.RemoveFromHand();
                if (e.TakeDamage(dmg)) {
                    i--;
                }
            }
            Player.Instance().UseAP(costAP);
            
        }
    }
}
