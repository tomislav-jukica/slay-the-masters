using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private static Player _instance;
    public BattleLog battleLog;
     
    public int maxHP;
    [SerializeField]
    private int currentHP;
    [SerializeField]
    private int maxAP;
    public int currentAP; 
    [SerializeField]
    private int regenerationAP;
    [SerializeField]
    private int drawSize;
    public int maxHandSize;

    public List<Card> deck, realDeck;
    public List<Card> hand;
    public List<Card> discardPile;

    public GameObject handGO, deckGO, discardPileGO;
    [Range(0f, 1f)][SerializeField]
    private float cardDrawSpeed = 0.2f;

    public Image hpSlider, apSlider;
    public Text hpText, apText;

    public static Player Instance() { return _instance; }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }        
        InstantiateCards();
        currentHP = maxHP;
        currentAP = maxAP;
    }
    private void Start() {
        
    }

    private void Update() {
        hpText.text = currentHP + "/" + maxHP;
        hpSlider.fillAmount = (float)currentHP / maxHP;

        apText.text = currentAP + "/" + maxAP;
        apSlider.fillAmount = (float)currentAP / maxAP;
    }


    public void InstantiateCards() {
        for (int i = 0; i < deck.Count; i++) { 
            Card c = Instantiate(deck[i], deckGO.transform);
            realDeck.Add(c);
        }
    }
    public IEnumerator Draw() {
        for (int i = 0; i < drawSize; i++) {
            if(hand.Count < maxHandSize) {
                DrawACard();
                yield return new WaitForSeconds(cardDrawSpeed);
            } else {
                battleLog.Write("Your hand is full!");
                break;
            }
            
        }
        
    }
    private void DrawACard() {
        if (realDeck.Count == 0) {
            while (discardPile.Count != 0) {
                Card c = discardPile[0];
                c.transform.SetParent(deckGO.transform);
                c.EnableCard();
                realDeck.Add(c);
                discardPile.Remove(c);
            }            
            //realDeck = ShuffleCards(deck);
        }
        Card nextCard = realDeck[0];
        hand.Add(nextCard);        
        realDeck.Remove(nextCard);
        nextCard.transform.SetParent(handGO.transform);

    }

    public static List<Card> ShuffleCards(List<Card> list) {
        for (int i = 0; i < list.Count; i++) {
            Card temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    public void TakeDamage(int amount) {
        currentHP -= amount;
        if(currentHP <= 0) {
            Die();
        }
        Debug.Log("Player took " + amount + " damage.");
    }

    private void Die() {
        BattleManager.Instance().BattleLost();
    }

    public void UseAP(int amount) {
        currentAP -= amount;
    }
    public void NewTurn() {
        currentAP += regenerationAP;
        if(currentAP > maxAP) {
            currentAP = maxAP;
        }
    }
}
