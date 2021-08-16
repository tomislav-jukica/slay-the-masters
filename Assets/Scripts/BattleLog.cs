using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLog : MonoBehaviour
{
    public GameObject battleLog;
    public Font font;
    public int fontSize = 30;

    public void Write(string msg) {
        GameObject nGO = new GameObject("BattleLog Message");
        Debug.Log("hey");
        nGO.transform.SetParent(battleLog.transform);
        Debug.Log("hey");
        Text text = nGO.AddComponent<Text>();
        Debug.Log("hey");
        text.text = msg;
        nGO.AddComponent<TextFadeout>();        
    }

    




}
