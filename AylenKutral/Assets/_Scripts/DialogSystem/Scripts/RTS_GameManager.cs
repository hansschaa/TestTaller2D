using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RTS_GameManager
{
    private static RTS_GameManager _Instance;
    public static RTS_GameManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new RTS_GameManager();
            }
            return _Instance;
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("RTS_MainMenu");
    }


    public RTS_HistoryFlow historyFlow;

    public void SetDialog(int i)
    {
        historyFlow.SetActual(stage,i);
    }
    
    public int stage = 0;
    public int person = 0;
    

    public void NewGame()
    {
        SceneManager.LoadScene("RTS_Dialog");
    }


    public void MenuScene()
    {
        SceneManager.LoadScene("RTS_MainMenu");
    }

    public void Room()
    {
        SceneManager.LoadScene("RTS_Room");
    }

    public void Dialog()
    {
        SceneManager.LoadScene("RTS_Dialog");
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
