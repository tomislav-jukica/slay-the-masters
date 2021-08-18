using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLog : MonoBehaviour
{
    public GameObject battleLog;
    public Font font;
    public int fontSize = 30;
    private Color? fontColor = Color.white;

    public void Write(string msg, Color? color = null) {
        if (color != null) fontColor = color;
        GameObject nGO = new GameObject("BattleLog Message");
        nGO.transform.SetParent(battleLog.transform);
        Text text = nGO.AddComponent<Text>();
        text.color = (Color)fontColor;
        text.text = msg;
        nGO.AddComponent<TextFadeout>();        
    }
}
