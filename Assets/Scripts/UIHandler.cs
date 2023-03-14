using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [Header("StatePanels")]

    [SerializeField]
    private GameObject menuPanelObject = null;

    [SerializeField]
    private GameObject ingamePanelObject = null;

    [SerializeField]
    private GameObject pausePanelObject = null;

    [SerializeField]
    private GameObject gameOverPanelObject = null;

    [SerializeField]
    private GameObject winPanelObject = null;

    [SerializeField]
    private GameObject win01Object = null;

    [SerializeField]
    private GameObject win02Object = null;

    [SerializeField]
    private GameObject win03Object = null;

    [Header("TextComponents")]

    [SerializeField]
    private TMP_Text startText;

    [SerializeField]
    private TMP_Text unpauseText;

    [SerializeField]
    private TMP_Text resetText;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private int topScore = 666;

    [SerializeField]
    private int midScore = 333;

    private void TurnOffObjects()
    {
        menuPanelObject.SetActive(false);
        ingamePanelObject.SetActive(false);
        pausePanelObject.SetActive(false);
        gameOverPanelObject.SetActive(false);
        winPanelObject.SetActive(false);

        win01Object.SetActive(false);
        win02Object.SetActive(false);
        win03Object.SetActive(false);

        startText.gameObject.SetActive(false);       
        unpauseText.gameObject.SetActive(false);
        resetText.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        scoreText.SetText(score.ToString());
    }

    public void SetUI(GameStates gameState)
    {
        TurnOffObjects();

        switch (gameState)
        {
            case GameStates.MENU:
                menuPanelObject.SetActive(true);
                //startText.gameObject.SetActive(true);
                break;
            case GameStates.INGAME:
                ingamePanelObject.SetActive(true);
                break;
            case GameStates.PAUSE:
                pausePanelObject.SetActive(true);
                //unpauseText.gameObject.SetActive(true);
                //resetText.gameObject.SetActive(true);
                break;
            case GameStates.GAMEOVER:
                winPanelObject.SetActive(true);
                //resetText.gameObject.SetActive(true);
                SetScoreState(0);
                break;
            case GameStates.WIN:
                winPanelObject.SetActive(true);
                //resetText.gameObject.SetActive(true);
                SetScoreState(GameManager.Score);
                break;
            default:
                break;
        }
    }
    
    private void SetScoreState(int score)
    {
        if(score > topScore)
        {
            win01Object.SetActive(true);
        }
        else if(score > midScore)
        {
            win02Object.SetActive(true);
        }
        else
        {
            win03Object.SetActive(true);
        }
    }
}
