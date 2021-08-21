using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour 
{
    private BattleManager BM;

    public string type;
    public int maxHP;
    public int currentHP;
    public int minDmg;
    public int maxDmg;

    public ActionType action;
    public int poison = 0;

    public Text hpText;
    public Image hpSlider;
    public Image actionImage;
    public Sprite attackIcon, slowIcon, passIcon, necromancyIcon;
    public Sprite poisonEffectSprite;
    public GameObject effectHolder;
    private GameObject poisonEffect, poisonNumber;

     


    private void Start() {
        BM = BattleManager.Instance();
        currentHP = maxHP;
    }
    private void Update() {
        hpText.text = currentHP + "/" + maxHP;
        hpSlider.fillAmount = (float)currentHP / maxHP;
        if(poison == 0) {
            Destroy(poisonEffect);
        }
        SetActionIcon();
    }

    public void Stuned() {
        action = ActionType.PASS;
        SetActionIcon();
    }
    public void Poisoned(int amount) {
        poison += amount;
        if (poisonEffect == null) {
            poisonEffect = new GameObject("PoisonEffect");
            Image image = poisonEffect.AddComponent<Image>();
            image.sprite = poisonEffectSprite;
            image.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);


            poisonNumber = new GameObject("Number");
            Text number = poisonNumber.AddComponent<Text>();
            number.alignment = TextAnchor.LowerCenter;
            number.font = Player.Instance().battleLog.font;
            number.text = poison.ToString();

            poisonNumber.transform.SetParent(poisonEffect.transform);
            poisonEffect.transform.SetParent(effectHolder.transform);
        } else {
            poisonNumber.GetComponent<Text>().text = poison.ToString();
        }
    }

    public int Attack() {
        return Random.Range(minDmg, maxDmg);
    }
    /// <summary>
    /// Reduces enemy health by amount inputed.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>Returns true if the enemy is killed.</returns>
    public bool TakeDamage(int amount) {
        currentHP -= amount;
        if (currentHP <= 0) {
            Die();
            return true;
        }
        return false;
    }
    public void Heal(int healAmount) {
        this.currentHP += healAmount;
        if (currentHP > maxHP) {
            currentHP = maxHP;
        }
    }

    public void Die() {
        BM.liveEnemies.Remove(this);        
        if (BM.liveEnemies.Count == 0) {
            BM.BattleWon();
        }
        Destroy(this.gameObject);

    }

    public abstract void ShowAction();
    public abstract void ExecuteAction();

    private void SetActionIcon() {
        switch (action) {
            case ActionType.ATTACK:
                actionImage.sprite = attackIcon;
                break;
            case ActionType.PASS:
                actionImage.sprite = passIcon;
                break;
            case ActionType.SLOW:
                actionImage.sprite = slowIcon;
                break;
            case ActionType.NECROMANCY:
                actionImage.sprite = necromancyIcon;
                break;
            default:
                actionImage.sprite = null;
                break;
        }
    }

    public enum ActionType {
        ATTACK,
        SLOW,
        PASS,
        NECROMANCY
    }
}
