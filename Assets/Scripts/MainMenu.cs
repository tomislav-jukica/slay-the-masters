using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Button btnContinue, btnNewGame, btnOptions, btnExit, btnChooseClass;
    public GameObject pickAClass, loadingScreen, optionsScreen;
    public Image loadingBar;    

    private void Awake() {
        
    }
    private void Start() {
        //btnContinue.onClick.AddListener(Continue);
        btnNewGame.onClick.AddListener(NewGame);
        btnOptions.onClick.AddListener(Options);
        btnExit.onClick.AddListener(Exit);
        btnChooseClass.onClick.AddListener(ChooseClass);
        AudioManager.Instance().Play("MainMenuTheme");
    }

    private void ChooseClass() {
        LoadScene(1);
    }

    private void Continue() {
        throw new NotImplementedException();
    }

    private void NewGame() {
        pickAClass.SetActive(true);

    }

    public void Options() {
        optionsScreen.SetActive(!optionsScreen.activeSelf);
    }

    private void Exit() {
        Application.Quit();
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
