using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMarker : MonoBehaviour
{
    GameManager GM;
    Button button;

    [SerializeField]
    private Vector2 markSize = new Vector2(75, 75);
    [SerializeField]
    private Vector2 markSizeLarge = new Vector2(125, 125);

    void Start()
    {
        GM = GameManager.Instance();
        button = this.GetComponent<Button>();
        button.onClick.AddListener(Clicked);
        
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
    }
    private void Deselect(MapMarker marker) {
        RectTransform rt = marker.GetComponent<RectTransform>();       
        rt.sizeDelta = markSize;
        GM.SelectMapMarker(null);        
    }
}
