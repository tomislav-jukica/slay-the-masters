using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowInTheSky : Card {
    [SerializeField]
    SkyArrow skyArrow;
    public override void Action() { 
        if(CheckIfParentIsEnemy()) {
            SkyArrow instance = Instantiate(skyArrow, Player.Instance().deckGO.transform);
            Player.Instance().realDeck.Add(instance);
            Draggable d = instance.GetComponent<Draggable>();
            d.card = instance;
            Player.Instance().UseAP(costAP);
            this.RemoveFromHand();
        } 
        
    }
}
