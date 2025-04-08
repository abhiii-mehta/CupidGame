using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject playerSelectPanel;
    public GameObject creditsPanel;
    public AudioSource audioSource;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (creditsPanel.activeSelf)
            {
                HideCredits();
            }
            else if (playerSelectPanel.activeSelf)
            {
                mainMenuPanel.SetActive(true);
                playerSelectPanel.SetActive(false);
            }
        }
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        playerSelectPanel.SetActive(true);
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ToggleSound()
    {
        if (audioSource != null)
        {
            audioSource.mute = !audioSource.mute;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
