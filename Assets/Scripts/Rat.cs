using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rat : Enemy {
   
    public override void ShowAction() {
        action = ActionType.ATTACK;
    }
    public override void ExecuteAction() {
        if(action == ActionType.ATTACK) {
            Player.Instance().TakeDamage(Attack());
        }
    }
}
