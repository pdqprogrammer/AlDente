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

    public static GameStates CurrentGameState => _Instance.gameState;

    /**UNITY FUNCTIONS**/
    private void Awake()
    {
        Instance = this;
        ChangeGameState(GameStates.INGAME);
    }

    private void Update()
    {
        //Listen for key press and reset the scene
        if (Input.GetKeyUp(KeyCode.Alpha0) /*&& gameState != GameStates.INGAME*/)
        {
            ResetGame();
        }

        if (Input.GetKeyUp(KeyCode.Escape) && gameState == GameStates.INGAME)
        {
            ChangeGameState(GameStates.PAUSE);
            return;
        }

        if (Input.GetKeyUp(KeyCode.Escape) && gameState == GameStates.PAUSE)
        {
            ChangeGameState(GameStates.INGAME);
            return;
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
