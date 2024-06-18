using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiLevelManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup panelWin;
    [SerializeField] private Button ButtonPlayAgain;
    [SerializeField] private Button ButtonNextLevel;

    [SerializeField] private CanvasGroup panelLose;
    [SerializeField] private Button ButtonPlayAgainLose;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private string namemainscene;
    [SerializeField] private string nameNextScene;

    // Start is called before the first frame update
    void Start()
    {
        panelWin.HideCanvasGroup();
        panelLose.HideCanvasGroup();
        backToMenuButton.onClick.AddListener(LoadMainMenu);
        backToMenuButton.onClick.AddListener(LoadMainMenu);
        ButtonPlayAgain.onClick.AddListener(RestartLevel);
        ButtonPlayAgainLose.onClick.AddListener(RestartLevel);
        ButtonNextLevel.onClick.AddListener(LoadNextLevel);
        Time.timeScale = 1F;
    }

    public void OnGameWin()
    {
        panelWin.ShowCanvasGroup();
        Time.timeScale = 0f;
    }

    public void OnGameLose()
    {
        panelLose.ShowCanvasGroup();
        Time.timeScale = 0f;
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nameNextScene);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(namemainscene);
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