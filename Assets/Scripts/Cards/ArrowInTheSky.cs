using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowInTheSky : Card {
    [SerializeField]
    SkyArrow skyArrow;
    public override void Action() { 
        if(CheckIfParentIsEnemy() || CheckIfParentIsPlayer()) {
            SkyArrow instance = Instantiate(skyArrow, Player.Instance().deckGO.transform);
            PlaySound();
            Player.Instance().realDeck.Add(instance);
            Draggable d = instance.GetComponent<Draggable>();
            d.card = instance;
            Player.Instance().UseAP(costAP);
            this.RemoveFromHand();
        }
        else {
            this.transform.SetParent(Player.Instance().handGO.transform);
        }
    }
}
