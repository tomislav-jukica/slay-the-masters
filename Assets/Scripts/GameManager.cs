using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Card> startingDeck;
    public List<Card> deck;
    public List<Card> cardRewards;

    private MapMarker selectedMapMarker;
    private static GameManager _instance;
    public static GameManager Instance() { return _instance; }

    public GameObject loadingScreen, endOfDemoScreen, optionsScreen;
    public Image loadingBar;
    public Button startButton;
    public int levelToPlay = -1;

    public Text questTitle, questDescription, questCounter;
    public Slider musicSlider, soundSlider;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
        startButton.onClick.AddListener(Play);
        AudioManager.Instance().Play("MapTheme");

        Debug.Log("test: " + AudioManager.Instance().GetMusicVolume());
        musicSlider.value = AudioManager.Instance().GetMusicVolume();






    }
    private void Start() {
        deck = new List<Card>(startingDeck);
    }
    private void Update() {
        if(levelToPlay < 0) {
            startButton.interactable = false;
            startButton.GetComponentInChildren<Text>().color = startButton.colors.disabledColor;
        } else {
            startButton.interactable = true;
            startButton.GetComponentInChildren<Text>().color = startButton.colors.normalColor;
        }
        if(levelToPlay == 13) {
            endOfDemoScreen.SetActive(true);
        }
    }
    public void ReturnToMenu() {
        LoadScene(0, true);
    }

    private void Play() {        
        LoadScene(levelToPlay);
        selectedMapMarker.levelIndex++;
        selectedMapMarker.currentBattle++;
        selectedMapMarker.Deselect(selectedMapMarker);
    }

    public void SelectMapMarker(MapMarker marker) {
        selectedMapMarker = marker;
        if(marker == null) {
            questTitle.text = "";
            questDescription.text = "";
            questCounter.text = "";
        } else {
            questTitle.text = marker.questTitle;
            questDescription.text = marker.questDescription;
            questCounter.text = marker.questCounter;
        }
        
    }
    public MapMarker GetSelectedMapMarker() {
        return selectedMapMarker;
    }

    public void LoadScene(int sceneIndex, bool killGM = false) {
        StartCoroutine(LoadSceneAsync(sceneIndex, killGM));
    }
    private IEnumerator LoadSceneAsync(int sceneIndex, bool killGM = false) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.fillAmount = progress;            
            yield return null;
        }
        loadingScreen.SetActive(false);
        if (killGM) Destroy(this.gameObject);
    }

    public void ToggleOptions() {
        Application.Quit();      
    }

}
