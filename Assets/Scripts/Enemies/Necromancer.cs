using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : Enemy
{
    public int slowAmount = 2;
    public Enemy summon;
    public override void ShowAction() {
        BattleManager BM = BattleManager.Instance();
        float rng = Random.Range(0f, 1f);
        if (BM.liveEnemies.Count < 3) {
            if(rng <= 0.33f) {
                action = ActionType.NECROMANCY;
            } else {
                rng = Random.Range(0f, 1f);
                if (rng <= 0.5f) {
                    action = ActionType.SLOW;
                }
                else {
                    action = ActionType.ATTACK;
                }
            }
        } else {
            rng = Random.Range(0f, 1f);
            if (rng <= 0.5f) {
                action = ActionType.SLOW;
            }
            else {
                action = ActionType.ATTACK;
            }
        } 
    }

    public override void ExecuteAction() {
        float chance = 1f;
        Player player = Player.Instance();
        if (player.GetPlayerEffect(Player.EffectType.HIDE) > 0) {
            chance = Random.Range(0f, 1f);
        }
        if (chance >= 0.5f) {
            if (action == ActionType.ATTACK) {
                player.TakeDamage(Attack());
            }
            else if (action == ActionType.SLOW) {
                player.TakeSlow(slowAmount);
            } else if(action == ActionType.NECROMANCY) {
                SummonUndead();
            }
        }
    }
    private void SummonUndead() {
        BattleManager BM = BattleManager.Instance();
        for (int i = 0; i < BM.enemySpaces.Count; i++) {
            Enemy e = BM.enemySpaces[i].GetComponentInChildren<Enemy>();
            Debug.Log(e);
            if (e == null) {
                Enemy s = Instantiate(summon, BM.enemySpaces[i].transform);
                BM.liveEnemies.Add(s);
                break;
            }
        }
    }
}
