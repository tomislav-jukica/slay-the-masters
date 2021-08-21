using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadAnotherLevel : MonoBehaviour
{
    public int nextLevelIndex;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image loadingBar;
    [SerializeField] Button continueButton;

    private void Awake() {
        continueButton.onClick.AddListener(() => { LoadScene(nextLevelIndex); });
    }
    public void LoadScene(int sceneIndex) {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }
    private IEnumerator LoadSceneAsync(int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.fillAmount = progress;
            yield return null;
        }
        loadingScreen.SetActive(false);
    }
}
