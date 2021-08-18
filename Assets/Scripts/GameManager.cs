using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private MapMarker selectedMapMarker;
    private static GameManager _instance;
    public static GameManager Instance() { return _instance; }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
    }    
    public void SelectMapMarker(MapMarker marker) {
        selectedMapMarker = marker;
    }
    public MapMarker GetSelectedMapMarker() {
        return selectedMapMarker;
    }

}
