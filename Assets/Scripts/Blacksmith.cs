using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blacksmith : MonoBehaviour
{
    [SerializeField]
    private Vector2 markSize = new Vector2(200, 200);
    [SerializeField]
    private Vector2 markSizeLarge = new Vector2(250, 250);
    [SerializeField]
    private Button attack, defense, continueButton;    
    [SerializeField] private Card attackUpgradeCard;
    [SerializeField] private Card defenseUpgradeCard;

    private UpgradeType upgrade = UpgradeType.NOTHING;

    private void Awake() {
        attack.onClick.AddListener(SelectedAttack);
        defense.onClick.AddListener(SelectedDefense);
        continueButton.onClick.AddListener(Continue);
    }
    private void Update() {
        if(upgrade == UpgradeType.NOTHING) {
            continueButton.interactable = false;
        } else {
            continueButton.interactable = true;
        }
    }
    private void SelectedAttack() {
        Select(attack);
        upgrade = UpgradeType.ATTACK;
        Deselect(defense);
    }
    private void SelectedDefense() {
        Select(defense);
        upgrade = UpgradeType.DEFENSE;
        Deselect(attack);
    }

    private void Select(Button marker) {
        RectTransform rt = marker.GetComponent<RectTransform>();
        rt.sizeDelta = markSizeLarge;
    }
    private void Deselect(Button marker) {
        RectTransform rt = marker.GetComponent<RectTransform>();
        rt.sizeDelta = markSize;
    }
    private void Continue() {
        LoadAnotherLevel load = this.gameObject.GetComponent<LoadAnotherLevel>();
        if(upgrade == UpgradeType.ATTACK) {
            UpgradeAttack();
        } else if(upgrade == UpgradeType.DEFENSE) {
            UpgradeDefense();
        }        
    }
    private void UpgradeAttack() {
        GameManager GM = GameManager.Instance();
        for(int i = 0; i < GM.deck.Count; i++ ) {
            Card c = GM.deck[i];
            if(c.cardName == "Basic Shot") {                
                GM.deck.Remove(c);
                GM.deck.Insert(0, attackUpgradeCard);
            }
        }
    }
    private void UpgradeDefense() {
        GameManager GM = GameManager.Instance();
        for (int i = 0; i < GM.deck.Count; i++) {
            Card c = GM.deck[i];
            if (c.cardName == "Basic Shield") {
                GM.deck.Remove(c);
                GM.deck.Insert(0, defenseUpgradeCard);
            }
        }
    }

    private enum UpgradeType {
        ATTACK,
        DEFENSE,
        NOTHING
    }
}
