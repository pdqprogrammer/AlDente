using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;

    private GameStates gameState = GameStates.START;

    /**UNITY FUNCTIONS**/
    private void Awake()
    {
        Instance = this;
    }

    ///PUBLIC FUNCTIONS///
    public static void ChangeGameState(GameStates state)
    {
        _Instance.gameState = state;
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
