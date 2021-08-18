using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rat : Enemy {
   
    public override void ShowAction() {
        action = ActionType.ATTACK;
    }
    public override void ExecuteAction() {
        float chance = 1f;
        if (Player.Instance().GetPlayerEffect(PlayerEffect.EffectType.HIDE) != null) {
            chance = Random.Range(0f, 1f);
        }
        if (chance >= 0.5f) {
            if (action == ActionType.ATTACK) {
                Player.Instance().TakeDamage(Attack());
            }
        }
    }
}
