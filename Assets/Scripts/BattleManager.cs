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

    private static BattleManager _instance;
    public static BattleManager Instance() { return _instance; }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
    }

    void Start()
    {
        player = Player.Instance();
        StartCoroutine(player.Draw());        
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
        foreach (Enemy e in liveEnemies) {
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
        victoryScreen.SetActive(true); 
    }
    public void BattleLost() {
        defeatScreen.SetActive(true);
    }
}
