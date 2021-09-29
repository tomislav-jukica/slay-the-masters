using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    Player player;

    public int turnNumber = 1;

    public List<Enemy> enemies;
    public List<GameObject> enemySpaces;
    public List<Enemy> liveEnemies;
    public Button endTurnButton;
    public Text turnNumberText;
    public GameObject victoryScreen;
    public GameObject defeatScreen;
    public bool rewardScreen;

    private static BattleManager _instance;
    public static BattleManager Instance() { return _instance; }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
        PlayMusic();        
    }

    private void PlayMusic() {
        string n = gameObject.scene.name;
        if (n == "Level 9") {
            AudioManager.Instance().Play("BossTheme");
        }
        else {
            AudioManager.Instance().Play("WoodTheme");
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.V)) {
            BattleWon();
        }
    }
    void Start()
    {
        player = Player.Instance();
        StartCoroutine(player.Draw(player.drawSize * 2));        
        SummonEnemies();
        ShowEnemyActions();

        turnNumberText.text = turnNumber.ToString();
        endTurnButton.onClick.AddListener(EndTurn);
    }

    private void EndTurn() {
        turnNumber += 1;
        turnNumberText.text = turnNumber.ToString();

        ApplayPoison();
        ExecuteEnemyActions();
        ShowEnemyActions();

        StartCoroutine(player.Draw());
        player.NewTurn();
    }

    public void SummonEnemies() {
        for (int i = 0; i < enemies.Count; i++) {
            Enemy enemy = enemies[i];
            Enemy liveEnemy = Instantiate(enemy, enemySpaces[i].transform);
            liveEnemies.Add(liveEnemy);
        }
    }

    public void ShowEnemyActions() {
        foreach (Enemy e in liveEnemies) {
            e.ShowAction();
        }
    }

    public void ExecuteEnemyActions() {
        for(int i = 0; i < liveEnemies.Count; i++) {
            Enemy e = liveEnemies[i];
            e.ExecuteAction();
        }
    }

    private void ApplayPoison() {
        for (int i = 0; i < liveEnemies.Count; i++) {
            Enemy e = liveEnemies[i];
            if(e.TakeDamage(e.poison)) {
                i--;
            }                        
        }
    }

    public void BattleWon() {
        rewardScreen = true;
        victoryScreen.SetActive(true);
        player.currentAP = player.maxAP; //to not have the cards disabled
        RewardManager rm = victoryScreen.GetComponent<RewardManager>();
        rm.CardRewards();
        AudioManager.Instance().Play("MapTheme");
    }
    public void BattleLost() {
        defeatScreen.SetActive(true);
        AudioManager.Instance().Play("MainMenuTheme");
    }
}
