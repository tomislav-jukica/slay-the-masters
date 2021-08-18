using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public int regenerationAP;
    [SerializeField]
    private int drawSize;
    public int maxHandSize;

    public List<Card> deck, realDeck;
    public List<Card> hand;
    public List<Card> discardPile;
    public List<PlayerEffect> playerEffects;

    public GameObject handGO, deckGO, discardPileGO;
    public GameObject playerEffectsGO;

    [Range(0f, 1f)][SerializeField]
    private float cardDrawSpeed = 0.2f;

    public Image hpSlider, apSlider;
    public Text hpText, apText;

    public Sprite slowEffectSprite;

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
            //TODO need to shuffle
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
        //Debug.Log("Player took " + amount + " damage.");
    }


    public void TakeSlow(int slowAmount) {
        bool exists = false;
        SlowEffect slow = null;

        foreach (SlowEffect item in playerEffects) {
            slow = item;
            exists = true;
        }
        if(exists) {
            slow.slowAmount += slowAmount;
        } else {
            GameObject nGO = new GameObject("SlowEffect");
            Image image = nGO.AddComponent<Image>();
            image.sprite = slowEffectSprite;

            slow = nGO.AddComponent<SlowEffect>();
            slow.slowAmount = slowAmount;

            nGO.transform.SetParent(playerEffectsGO.transform);
            playerEffects.Add(slow);
        }
    }

    private void Die() {
        BattleManager.Instance().BattleLost();
    }

    public void UseAP(int amount) {
        currentAP -= amount;
    }
    public void NewTurn() {
        int tempRegenerationAP = regenerationAP;
        SlowEffect slow = (SlowEffect)GetPlayerEffect(PlayerEffect.EffectType.SLOW);
        if (slow != null) {
            tempRegenerationAP -= slow.slowAmount;
            if (tempRegenerationAP < 0) tempRegenerationAP = 0;
            slow.slowAmount--;
            slow.number.text = slow.slowAmount.ToString();
            if(slow.slowAmount == 0) {
                Destroy(slow.gameObject);
            }
        }
        currentAP += tempRegenerationAP;

        if(currentAP > maxAP) {
            currentAP = maxAP;
        }
        
    }
    private PlayerEffect GetPlayerEffect(PlayerEffect.EffectType effectType) {
        foreach (PlayerEffect item in playerEffects) {
            if(item.type == effectType) {
                return item;
            } 
        }
        return null;
    }

    private void CheckForPlayerEffects() {
        for(int i = 0; i < playerEffects.Count; i++) {
            PlayerEffect e = playerEffects[i];
            e.Activate();
        }
    }
}
