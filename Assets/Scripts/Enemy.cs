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

    public Text hpText;
    public Image hpSlider;
    public Image actionImage;
    public Sprite attackIcon;
    public Sprite passIcon;


    private void Start() {
        BM = BattleManager.Instance();
        currentHP = maxHP;
    }
    private void Update() {
        hpText.text = currentHP + "/" + maxHP;
        hpSlider.fillAmount = (float)currentHP / maxHP;
        SetActionIcon();


    }

    public int Attack() {
        return Random.Range(minDmg, maxDmg);
    }

    public void TakeDamage(int amount) {
        currentHP -= amount;
        if (currentHP <= 0) Die();
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
            default:
                actionImage.sprite = null;
                break;
        }
    }

    public enum ActionType {
        ATTACK,
        PASS
    }
}
