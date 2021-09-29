using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMarker : MonoBehaviour
{
    GameManager GM;
    Button button;

    public string questTitle;
    [TextArea(1, 5)]
    public string questDescription;
    public string questCounter;
    public bool isBattle = true;

    [SerializeField]
    private Vector2 markSize = new Vector2(75, 75);
    [SerializeField]
    private Vector2 markSizeLarge = new Vector2(125, 125);

    public int levelIndex;
    public int maxLevel;
    public int currentBattle = 0;
    public int numberOfBattles = 0;
    [SerializeField]
    private List<MapMarker> levelsUnlocked;

    void Start()
    {
        GM = GameManager.Instance();
        button = this.GetComponent<Button>();
        button.onClick.AddListener(Clicked);
        
    }
    private void Update() {
        if(isBattle) {
            questCounter = "Battles fought " + currentBattle + "/" + numberOfBattles;
        }
        
        if(levelIndex == maxLevel) {
            UnlockLevels();
            Destroy(this.gameObject);
        }
    }

    private void UnlockLevels() {
        foreach (MapMarker level in levelsUnlocked) {
            level.gameObject.SetActive(true);
        }
    }
    private void Clicked() {
        if(GM.GetSelectedMapMarker() != null) {
            Deselect(GM.GetSelectedMapMarker());            
        }
        Select(this);
    }

    private void Select(MapMarker marker) {
        RectTransform rt = marker.GetComponent<RectTransform>();
        rt.sizeDelta = markSizeLarge;
        GM.SelectMapMarker(marker);
        GM.levelToPlay = levelIndex;
    }
    public void Deselect(MapMarker marker) {
        RectTransform rt = marker.GetComponent<RectTransform>();       
        rt.sizeDelta = markSize;
        GM.SelectMapMarker(null);
        GM.levelToPlay = -1;
    }
}
