using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiLevelManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup panelWin; // Panel für Gewinnanzeige
    [SerializeField] private Button ButtonPlayAgain; // Button zum Neustart
    [SerializeField] private Button ButtonNextLevel; // Button für nächstes Level

    [SerializeField] private CanvasGroup panelLose; // Panel für Verlustanzeige
    [SerializeField] private Button ButtonPlayAgainLose; // Button zum Neustart 
    [SerializeField] private Button backToMenuButton; // Button für Hauptmenü
    [SerializeField] private string namemainscene; // Name der Hauptmenü-Szene
    [SerializeField] private string nameNextScene; // Name der nächsten Level-Szene

    void Start()
    {
        panelWin.HideCanvasGroup();
        panelLose.HideCanvasGroup();
        backToMenuButton.onClick.AddListener(LoadMainMenu);
        ButtonPlayAgain.onClick.AddListener(RestartLevel); 
        ButtonPlayAgainLose.onClick.AddListener(RestartLevel);
        ButtonNextLevel.onClick.AddListener(LoadNextLevel);
        Time.timeScale = 1F;
    }

    public void OnGameWin()
    {
        panelWin.ShowCanvasGroup(); // Zeigt das Gewinn-Panel
        Time.timeScale = 0f; // Stoppt die Zeit
    }

    public void OnGameLose()
    {
        panelLose.ShowCanvasGroup();
        Time.timeScale = 0f;
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Lädt das aktuelle Level neu
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nameNextScene); // Lädt das nächste Level
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(namemainscene); // Lädt die Hauptmenü-Szene
    }
}

public static class UiExtentions
{
    public static void HideCanvasGroup(this CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0f; 
        canvasGroup.interactable = false; 
        canvasGroup.blocksRaycasts = false; 
    }
    public static void ShowCanvasGroup(this CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1f; 
        canvasGroup.interactable = true; 
        canvasGroup.blocksRaycasts = true; 
    }
}
