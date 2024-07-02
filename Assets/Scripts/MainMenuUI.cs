using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _GameStateText;


    // Subscribe to event
    public void Awake()
    {
        TheDirector.OnGameStateChanged += TheDirectorOnGameStateChanged;
    }

    //leave event
    void OnDestroy()
    {
        TheDirector.OnGameStateChanged -= TheDirectorOnGameStateChanged;
    }

    // what happens whe the state has changed
    private void TheDirectorOnGameStateChanged(TheDirector.GameState state)
    {
        _GameStateText.text = state.ToString();

        //example for turning UI on and off
        //_GameStateText.gameObject.SetActive(state == TheDirector.GameState.Player);
    }

    public void ReadyForWave()
    {
        //blah blah blah. Things happen like UI changes INSIDE the OnGameStateChanged method
        //example of switching states

        TheDirector.instance.UpdateGameState(TheDirector.GameState.Wave);
    }

    //Get us to the game portion.
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        Debug.Log("Options Clicked");
    }
    //quit game or stop playing in the editor.
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
