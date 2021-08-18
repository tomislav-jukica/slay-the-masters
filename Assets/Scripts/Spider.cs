using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public int slowAmount = 2;
    public Sprite slowIcon;
    public override void ShowAction() {
        float rng = Random.Range(0f, 1f);
        if(rng <= 0.25) {
            action = ActionType.SLOW;
        } else {
            action = ActionType.ATTACK;
        }        
    }
    public override void ExecuteAction() {
        if (action == ActionType.ATTACK) {
            Player.Instance().TakeDamage(Attack());
        } else if (action == ActionType.SLOW) {
            Player.Instance().TakeSlow(slowAmount);
        }
        
    }
}
