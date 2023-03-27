using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;

    [SerializeField]
    private UIHandler uIHandler = null;

    [SerializeField]
    private GameStates gameState = GameStates.INGAME;

    [SerializeField]
    private string sceneName;
    private int score = 0;
    private float playTime = 0.0f;
    private bool isItalian = false;

    public static GameStates CurrentGameState => _Instance.gameState;
    public static int Score => _Instance.score;
    public static float PlayTime => _Instance.playTime;
    public static bool IsItalian => _Instance.isItalian;

    /**UNITY FUNCTIONS**/
    private void Awake()
    {
        Instance = this;
        ChangeGameState(CurrentGameState);
        LanguageSaveData languageSaveData = SaveSystem.LoadData(true);

        if(languageSaveData != null)
        {
            isItalian = languageSaveData.IsItalian;
        }
        else
        {
            SaveSystem.SaveData(isItalian);
        }

        uIHandler.SetLanguageSettings(isItalian);
    }

    private void Update()
    {
        //Listen for key press and reset the scene
        if (Input.GetKeyUp(KeyCode.Escape) && gameState != GameStates.INGAME)
        {
            if (gameState == GameStates.MENU)
            {
                Application.Quit();
            }
            else
            {
                ResetGame();
            }
        }

        if (Input.GetKeyUp(KeyCode.P) && gameState == GameStates.INGAME)
        {
            ChangeGameState(GameStates.PAUSE);
            return;
        }

        if (Input.GetKeyUp(KeyCode.P) && gameState == GameStates.PAUSE)
        {
            ChangeGameState(GameStates.INGAME);
            return;
        }

        if (Input.GetKeyUp(KeyCode.Space) && gameState == GameStates.MENU)
        {
            ChangeGameState(GameStates.INGAME);
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            isItalian = !isItalian;
            uIHandler.SetLanguageSettings(isItalian);
            SaveSystem.SaveData(isItalian);
        }

        if (gameState == GameStates.INGAME)
        {
            playTime += Time.deltaTime;
        }
    }

    ///PUBLIC FUNCTIONS///

    //function to change the game state and can do certain functions based on the state...
    public static void ChangeGameState(GameStates state)
    {
        _Instance.gameState = state;
        _Instance.uIHandler?.SetUI(state);
    }

    //function to increase game score
    public static void IncreaseScore(int increase)
    {
        _Instance.score += increase;
        _Instance.uIHandler?.UpdateScore(_Instance.score);
    }

    //function to reset game score
    public static void ResetScore()
    {
        _Instance.score = 0;
    }

    //function to reset game
    public static void ResetGame()
    {
        SceneManager.LoadSceneAsync(_Instance.sceneName, LoadSceneMode.Single);
    }

    ///SETUP SINGLETON SAFELY///
    private static GameManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                Debug.LogError("GameManager getter - Attempt to get value of GameManager before it has been set.");
                return null;
            }
            return _Instance;
        }
        set
        {
            if (_Instance != null)
            {
                Debug.LogError("GameManager setter - Attempt to set GameManager when it has already been set.");
            }
            _Instance = value;
        }
    }
}
