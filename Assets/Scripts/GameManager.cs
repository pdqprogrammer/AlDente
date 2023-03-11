using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;

    private GameStates gameState = GameStates.INGAME;
    private int score = 0;

    /**UNITY FUNCTIONS**/
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //Listen for key press and reset the scene
        if (Input.GetKey(KeyCode.Alpha0) /*&& gameState != GameStates.INGAME*/)
        {
            SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
        }
    }

    ///PUBLIC FUNCTIONS///

    //function to change the game state and can do certain functions based on the state...
    public static void ChangeGameState(GameStates state)
    {
        _Instance.gameState = state;

        switch (state)
        {
            case GameStates.INGAME:
                break;
            case GameStates.MENU:
                break;
            case GameStates.GAMEOVER:
                break;
            case GameStates.WIN:
                break;
            default:
                break;
        }
    }

    //function to increase game score
    public static void IncreaseScore(int increase)
    {
        _Instance.score += increase;
    }

    //function to reset game score
    public static void ResetScore()
    {
        _Instance.score = 0;
    }

    //TODO create reset function that will revert everything back to its original state

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
