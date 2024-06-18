using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainpanel;
    [SerializeField] private Button startnewGamebutton;
    [SerializeField] private Button levelselectionbutton;
    
    [SerializeField] private CanvasGroup levelSelectionPanel;
    [SerializeField] private Button level1button;
    [SerializeField] private Button level2button;
    [SerializeField] private Button level3button;
    [SerializeField] private Button quitlevelselectionbutton;
    
    [SerializeField] private Button QUitgamebutton;
    [SerializeField] private string nameNextScene1;
    [SerializeField] private string nameNextScene2;
    [SerializeField] private string nameNextScene3;

    void Start()
    {
        // Panel und Button Listener zuweisen
        levelSelectionPanel.HideCanvasGroup();
        startnewGamebutton.onClick.AddListener(LoadLevel1);
        levelselectionbutton.onClick.AddListener(openLevelPanel);
        quitlevelselectionbutton.onClick.AddListener(closeLevelPanel);
        level1button.onClick.AddListener(LoadLevel1);
        
        level2button.onClick.AddListener(LoadLevel2);
        level3button.onClick.AddListener(LoadLevel3);
        QUitgamebutton.onClick.AddListener(quitgame);

        // Überprüfen und aktivieren der Level-Buttons
        level2button.interactable = false;
        if (PlayerPrefs.HasKey(nameNextScene2))
        {
            if (PlayerPrefs.GetInt(nameNextScene2) == 1)
            {
                level2button.interactable = true;
            }
        }
        
        level3button.interactable = false;
        if (PlayerPrefs.HasKey(nameNextScene3))
        {
            if (PlayerPrefs.GetInt(nameNextScene3) == 1)
            {
                level3button.interactable = true;
            }
        }
    }

    void Update()
    {
    }

    void closeLevelPanel()
    {
        levelSelectionPanel.HideCanvasGroup(); // Levelauswahl-Panel verstecken
        mainpanel.ShowCanvasGroup(); // Hauptmenü-Panel anzeigen
    }

    void openLevelPanel()
    {
        levelSelectionPanel.ShowCanvasGroup(); // Levelauswahl-Panel anzeigen
        mainpanel.HideCanvasGroup(); // Hauptmenü-Panel verstecken
    }
    
    void LoadLevel1()
    {
        SceneManager.LoadScene(nameNextScene1); // Level 1 laden
    }
   
    void LoadLevel2()
    {
        SceneManager.LoadScene(nameNextScene2); // Level 2 laden
    }
    
    void LoadLevel3()
    {
        SceneManager.LoadScene(nameNextScene3); // Level 3 laden
    }

    public void quitgame()
    {
        Application.Quit(); // Spiel beenden
        Debug.Log(message:"gameclosed");
    }
}
