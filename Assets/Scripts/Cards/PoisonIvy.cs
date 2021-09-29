using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonIvy : Card {
    public int poison;    
    public override void Action() { 
        if (CheckIfParentIsEnemy()) {
            PlaySound();
            for (int i = 0; i < BattleManager.Instance().liveEnemies.Count; i++) {
                Enemy e = BattleManager.Instance().liveEnemies[i];
                this.RemoveFromHand();
                e.Poisoned(poison);
            }
            Player.Instance().UseAP(costAP);
        }
        else {
            this.transform.SetParent(Player.Instance().handGO.transform);
        }
    }
}
