using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    private GameManager GM;
    public GameObject rewardHolder;
    public List<Card> rewards = new List<Card>();

    private void Awake() {
        GM = GameManager.Instance();
    }
    public void CardRewards() {
        while(rewards.Count < 3) {
            Card c = GetRandomCardReward();
            c.EnableCard();
            //c.GetComponent<Draggable>().reward = true;
            if (!rewards.Contains(c)) {                
                rewards.Add(c);                
                GameObject nGO = new GameObject(c.cardName);
                Button b = nGO.AddComponent<Button>();
                b.gameObject.AddComponent<RectTransform>();
                b.onClick.AddListener(() => { PickACardReward(c); });
                Instantiate(c, nGO.transform);
                nGO.transform.SetParent(rewardHolder.transform);
            }
        }
    }
    private Card GetRandomCardReward() {
        Card reward = null;
        List<Card> rewardList = new List<Card>(GM.cardRewards);
        int rng = Random.Range(0, rewardList.Count - 1);
        reward = rewardList[rng];
        return reward;
    }
    public void PickACardReward(Card card) {
        GameManager.Instance().deck.Add(card);
        GameManager.Instance().LoadScene(1);
    }
}
