using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectOfRabbit : Card {
    public int cards;
    public override void Action() {
        PlaySound();
        StartCoroutine(Work());
    }
    private IEnumerator Work() {
        if (CheckIfParentIsPlayer()) {
            StartCoroutine(Player.Instance().Draw(2));
            Player.Instance().UseAP(costAP);
            this.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
            yield return new WaitForSeconds(Player.Instance().cardDrawSpeed * cards);            
            this.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
            this.RemoveFromHand();
        }
        else {
            this.transform.SetParent(Player.Instance().handGO.transform);
        }
    }

}
