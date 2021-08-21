using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private static Player _instance;
    public BattleLog battleLog;
    public GameManager GM;
     
    public int maxHP;
    [SerializeField]
    private int currentHP;
    public int maxAP;
    public int currentAP;
    public int regenerationAP;
    public int drawSize;
    public int maxHandSize;



    public List<Card> deck, realDeck;
    public List<Card> hand;
    public List<Card> discardPile;

    public GameObject handGO, deckGO, discardPileGO;
    public GameObject playerEffectsGO, slowEffectGO, hideEffectGO, armorGO;

    [Range(0f, 1f)]
    public float cardDrawSpeed = 0.2f;

    [SerializeField]
    private int slowEffect = 0;
    private int hideEffect = 0;
    public int armor = 0;

    public Image hpSlider, apSlider;
    public Text hpText, apText;

    public Sprite slowEffectSprite, hideEffectSprite, armorSprite;

    public static Player Instance() { return _instance; }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
        GM = GameManager.Instance();
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
        deck = new List<Card>(GM.deck);
        deck = new List<Card>(ShuffleCards(deck));        
        for (int i = 0; i < deck.Count; i++) { 
            Card c = Instantiate(deck[i], deckGO.transform);
            realDeck.Add(c);
        }
    }
    public IEnumerator Draw(int draw = -1) {
        if (draw < 0) draw = drawSize;

        for (int i = 0; i < draw; i++) {
            if(hand.Count < maxHandSize) {
                DrawACard();
                yield return new WaitForSeconds(cardDrawSpeed);
            } else {
                battleLog.Write("Your hand is full!");
                break;
            }
            
        }        
    }
    public void DrawACard() {
        if (realDeck.Count == 0) {
            while (discardPile.Count != 0) {
                Card c = discardPile[0];
                c.transform.SetParent(deckGO.transform);
                c.EnableCard();
                realDeck.Add(c);
                discardPile.Remove(c);
            }
            
            realDeck = new List<Card>(ShuffleCards(realDeck)); 
        }
        Card nextCard = realDeck[0];
        hand.Add(nextCard);        
        realDeck.Remove(nextCard);
        nextCard.transform.SetParent(handGO.transform);

    }
    public static List<Card> ShuffleCards(List<Card> list) {
        for (int i = 0; i < list.Count; i++) {
            Card temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    public void TakeDamage(int amount) {
        armor -= amount;
        if(armorGO != null) {
            armorGO.GetComponentInChildren<Text>().text = armor.ToString();
        }        
        if(armor <= 0) {
            currentHP += armor;
            armor = 0;
            Destroy(armorGO.gameObject);
        }
        if(currentHP <= 0) {
            Die();
        }
    }
    public void Heal(int healAmount) {
        this.currentHP += healAmount;
        if(currentHP > maxHP) {
            currentHP = maxHP;
        }
    }

    public void TakeSlow(int slowAmount) {
        if(slowEffect > 0) {
            slowEffect += slowAmount;
            slowEffectGO.GetComponentInChildren<Text>().text = slowEffect.ToString();
        } else {
            slowEffect += slowAmount;
            slowEffectGO = new GameObject("SlowEffect");            
            Image image = slowEffectGO.AddComponent<Image>();
            slowEffectGO.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            image.sprite = slowEffectSprite;

            GameObject effectNumber = new GameObject("EffectNumber");
            Text number = effectNumber.gameObject.AddComponent<Text>();
            number.font = battleLog.font;
            number.alignment = TextAnchor.LowerCenter;
            number.rectTransform.sizeDelta = new Vector2(100, 90);
            number.text = slowEffect.ToString();
            effectNumber.transform.SetParent(slowEffectGO.transform);

            slowEffectGO.transform.SetParent(playerEffectsGO.transform);
        }
    }
    public void Hide(int turns) {     
        if (hideEffect > 0) {
            hideEffect += turns;
            hideEffectGO.GetComponentInChildren<Text>().text = hideEffect.ToString();
        }
        else {
            hideEffect += turns;
            hideEffectGO = new GameObject("HideEffect");
            Image image = hideEffectGO.AddComponent<Image>();
            hideEffectGO.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            image.sprite = hideEffectSprite;

            GameObject effectNumber = new GameObject("EffectNumber");
            Text number = effectNumber.gameObject.AddComponent<Text>();
            number.font = battleLog.font;
            number.alignment = TextAnchor.LowerCenter;
            number.rectTransform.sizeDelta = new Vector2(100, 90);
            effectNumber.transform.SetParent(hideEffectGO.transform);
            number.text = hideEffect.ToString();

            hideEffectGO.transform.SetParent(playerEffectsGO.transform);
        }
    }
    public void AddArmor(int amount) {

        if (armor > 0) {
            armor += amount;
            armorGO.GetComponentInChildren<Text>().text = armor.ToString();
        }
        else {
            armor += amount;
            armorGO = new GameObject("ArmorEffect");
            Image image = armorGO.AddComponent<Image>();
            armorGO.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            image.sprite = armorSprite;

            GameObject effectNumber = new GameObject("EffectNumber");
            Text number = effectNumber.gameObject.AddComponent<Text>();
            number.font = battleLog.font;
            number.alignment = TextAnchor.LowerCenter;
            number.rectTransform.sizeDelta = new Vector2(100, 90);
            effectNumber.transform.SetParent(armorGO.transform);
            number.text = armor.ToString();

            armorGO.transform.SetParent(playerEffectsGO.transform);
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

        if (slowEffect > 0) {
            tempRegenerationAP -= slowEffect;
            if (tempRegenerationAP < 0) tempRegenerationAP = 0;
            slowEffect--;
            slowEffectGO.GetComponentInChildren<Text>().text = slowEffect.ToString();

            if (slowEffect == 0) {
                Destroy(slowEffectGO.gameObject);
            }
        }
        if(hideEffect > 0) {
            hideEffect--;
            hideEffectGO.GetComponentInChildren<Text>().text = hideEffect.ToString();
            if(hideEffect == 0) {
                Destroy(hideEffectGO.gameObject);
            }
        }
        currentAP += tempRegenerationAP;

        if(currentAP > maxAP) {
            currentAP = maxAP;
        }
        
    }

    public int GetPlayerEffect(EffectType type) {
        switch(type) {
            case EffectType.HIDE:
                return hideEffect;
            case EffectType.SLOW:
                return hideEffect;
            default:
                Debug.LogError("Player doesn't have that type of effect.");
                return -1;
        }
    }

    public enum EffectType {
        SLOW,
        HIDE
    }
}
