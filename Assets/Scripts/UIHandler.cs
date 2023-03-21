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
    private GameObject winPanelObject = null;

    [Header("TextComponents")]

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private int topScore = 666;

    [SerializeField]
    private int midScore = 333;

    [SerializeField]
    private List<GameObject> englishLanguageObjects = new List<GameObject>();

    [SerializeField]
    private List<GameObject> italianLanguageObjects = new List<GameObject>();

    [SerializeField]
    private GameObject[] englishWinObjects = new GameObject[3];
    [SerializeField]
    private GameObject[] italianWinObjects = new GameObject[3];

    private bool isItalian = false;

    private void TurnOffObjects()
    {
        menuPanelObject.SetActive(false);
        ingamePanelObject.SetActive(false);
        pausePanelObject.SetActive(false);
        winPanelObject.SetActive(false);

        for(int i=0; i<englishWinObjects.Length; i++)
        {
            englishWinObjects[i].SetActive(false);
        }

        for (int i = 0; i < italianWinObjects.Length; i++)
        {
            italianWinObjects[i].SetActive(false);
        }
    }

    public void UpdateScore(int score)
    {
        if(score > topScore)
        {
            if (ColorUtility.TryParseHtmlString("#EFCE29FF", out Color color))
            {
                scoreText.color = color;
            }
        }

        scoreText.SetText(score.ToString());
    }

    public void SetUI(GameStates gameState)
    {
        TurnOffObjects();

        switch (gameState)
        {
            case GameStates.MENU:
                menuPanelObject.SetActive(true);
                break;
            case GameStates.INGAME:
                ingamePanelObject.SetActive(true);
                break;
            case GameStates.PAUSE:
                pausePanelObject.SetActive(true);
                break;
            case GameStates.GAMEOVER:
                winPanelObject.SetActive(true);
                SetScoreState(0);
                break;
            case GameStates.WIN:
                winPanelObject.SetActive(true);
                SetScoreState(GameManager.Score);
                break;
            default:
                break;
        }
    }

    public void SetLanguageSettings(bool italian)
    {
        //TODO turn on/off italian/english
        isItalian = italian;

        foreach (GameObject englishObject in englishLanguageObjects)
        {
            englishObject.SetActive(!italian);
        }

        foreach (GameObject italianObject in italianLanguageObjects)
        {
            italianObject.SetActive(italian);
        }

        if(GameManager.CurrentGameState == GameStates.WIN || GameManager.CurrentGameState == GameStates.GAMEOVER)
        {
            SetScoreState(GameManager.Score);
        }
    }
    
    private void SetScoreState(int score)
    {
        //turn off old objects
        for(int i=0; i<englishWinObjects.Length; i++)
        {
            englishWinObjects[i].SetActive(false);
        }

        for (int i = 0; i < italianWinObjects.Length; i++)
        {
            italianWinObjects[i].SetActive(false);
        }

        GameObject[] winLanguageObjects;

        if (isItalian)
        {
            winLanguageObjects = italianWinObjects;
        }
        else
        {
            winLanguageObjects = englishWinObjects;
        }

        if (score > topScore)
        {
            winLanguageObjects[0].SetActive(true);
        }
        else if(score > midScore)
        {
            winLanguageObjects[1].SetActive(true);
        }
        else
        {
            winLanguageObjects[2].SetActive(true);
        }
    }
}
