using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuPanel;

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button backButton;

    public void Start()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        SoundManager.Instance.PlayLoop("background_music");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsButton.animator.Rebind();

        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        backButton.animator.Rebind();

        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
