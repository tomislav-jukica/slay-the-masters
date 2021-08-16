using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeout : MonoBehaviour
{
    private Font font;
    private int fontSize;
    public float showTime = 2f;
    public float fadeTime = 2f;
    
    void Start()
    {
        font = this.GetComponentInParent<BattleLog>().font;
        fontSize = this.GetComponentInParent<BattleLog>().fontSize;
        StartCoroutine(StartFadeout());
    }

    private IEnumerator StartFadeout() {
        Text text = this.GetComponent<Text>();
        text.font = font;
        text.alignment = TextAnchor.UpperCenter;
        text.fontSize = fontSize;
        yield return new WaitForSeconds(showTime);
        StartCoroutine(FadeTextToZeroAlpha(fadeTime, text));
        yield return new WaitForSeconds(fadeTime);
        Destroy(this);
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i) {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i) {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

}
