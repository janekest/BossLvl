using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Das Pausenmenü UI-Element

    private InputAction pauseAction;
    private bool isPaused = false;

    private void Awake()
    {
        // Find the Input Action asset and get the "Pause" action
        var inputActionAsset = GetComponent<PlayerInput>().actions;
        pauseAction = inputActionAsset["UI/Pause"];
    }

    private void OnEnable()
    {
        // Subscribe to the pause action
        pauseAction.performed += OnPause;
    }

    private void OnDisable()
    {
        // Unsubscribe from the pause action
        pauseAction.performed -= OnPause;
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
