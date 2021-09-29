using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public abstract class Card : MonoBehaviour
{
    public string cardName;
    public int costAP;
    [TextArea(3, 10)]
    public string desc;

    [Range(0, 1)]
    [SerializeField]
    private float disabledCardOpacity = 0.5f;

    public Text cardNameText, cardAPText, cardDescriptionText;
    public Image cardImage, artImage;

    protected AudioSource audioSource;
    public AudioMixerGroup mixerGroup;
    public AudioClip sfx;

    private void Awake() {
        cardNameText.text = cardName;
        cardAPText.text = costAP.ToString();
        cardDescriptionText.text = desc;

        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.outputAudioMixerGroup = mixerGroup;
        audioSource.clip = sfx;
    }
    private void Update() {
        if(!BattleManager.Instance().rewardScreen) {
            if (Player.Instance().currentAP < costAP) {
                DisableCard();
            }
            else {
                EnableCard();
            }
        }
        
    }
    protected void PlaySound() {        
        audioSource.Play();
    }
    public abstract void Action();
    public void Banish() {
        Player.Instance().discardPile.Remove(this);
        Destroy(this.gameObject);
    }
    public void RemoveFromHand() {
        Card discardedCard = this;

        Player.Instance().discardPile.Add(discardedCard);
        Player.Instance().hand.Remove(discardedCard);
        this.transform.SetParent(Player.Instance().discardPileGO.transform);
    }

    public void DisableCard() {
        Color tempColor = cardImage.color;
        tempColor.a = disabledCardOpacity;
        cardImage.color = tempColor;

        tempColor = artImage.color;
        tempColor.a = disabledCardOpacity;
        artImage.color = tempColor;

        this.GetComponent<Draggable>().enabled = false;
    }

    public void EnableCard() {
        Color tempColor = cardImage.color;
        tempColor.a = 1f;
        cardImage.color = tempColor;

        tempColor = artImage.color;
        tempColor.a = 1f;
        artImage.color = tempColor;

        this.GetComponent<Draggable>().enabled = true;
    }

    

    public bool CheckIfParentIsEnemy() {
        foreach (Enemy e in BattleManager.Instance().liveEnemies) {
            if(this.transform.parent == e.transform) {
                return true;
            }
        }
        return false;
    }
    public bool CheckIfParentIsPlayer() {
        if(this.transform.parent == Player.Instance().transform) {
            return true;
        }
        return false;
    }

}
