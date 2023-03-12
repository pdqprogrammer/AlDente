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

    [Header("TextComponents")]

    [SerializeField]
    private TMP_Text startText;

    [SerializeField]
    private TMP_Text unpauseText;

    [SerializeField]
    private TMP_Text resetText;

    [SerializeField]
    private TMP_Text scoreText;

    private void TurnOffObjects()
    {
        menuPanelObject.SetActive(false);
        ingamePanelObject.SetActive(false);
        pausePanelObject.SetActive(false);
        gameOverPanelObject.SetActive(false);
        winPanelObject.SetActive(false);

        startText.gameObject.SetActive(false);       
        unpauseText.gameObject.SetActive(false);
        resetText.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        if(int.TryParse(scoreText.text, out int currScore))
        {
            currScore += score;
            scoreText.SetText(currScore.ToString());
        }
    }

    public void SetUI(GameStates gameState)
    {
        TurnOffObjects();

        switch (gameState)
        {
            case GameStates.MENU:
                menuPanelObject.SetActive(true);
                startText.gameObject.SetActive(true);
                break;
            case GameStates.INGAME:
                ingamePanelObject.SetActive(true);
                break;
            case GameStates.PAUSE:
                pausePanelObject.SetActive(true);
                unpauseText.gameObject.SetActive(true);
                resetText.gameObject.SetActive(true);
                break;
            case GameStates.GAMEOVER:
                gameOverPanelObject.SetActive(true);
                resetText.gameObject.SetActive(true);
                break;
            case GameStates.WIN:
                winPanelObject.SetActive(true);
                resetText.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
