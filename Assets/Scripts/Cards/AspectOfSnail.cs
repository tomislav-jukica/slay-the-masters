using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectOfSnail : Card {
    public int slow;
    public int armor;
    public override void Action() {
        if(CheckIfParentIsPlayer()) {
            Player player = Player.Instance();
            player.TakeSlow(slow);
            player.AddArmor(armor);
            player.UseAP(costAP);
            this.RemoveFromHand();
        }
        else {
            this.transform.SetParent(Player.Instance().handGO.transform);
        }
    }
}
