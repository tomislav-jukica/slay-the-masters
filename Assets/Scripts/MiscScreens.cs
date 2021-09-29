using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiscScreens : MonoBehaviour
{
    public Button quitButton, tryAgainButton;
    private void Awake() {
        if(quitButton != null) {
            quitButton.onClick.AddListener(Quit);
        }
        if (tryAgainButton != null) {
            tryAgainButton.onClick.AddListener(TryAgain);
        }
    }

    public void TryAgain() {
        GameManager.Instance().LoadScene(0, true);
    }
    public void Quit() {
        Application.Quit();
    }

}
