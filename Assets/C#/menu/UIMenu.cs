using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    private Button playButton;
    private Button settingsButton;
    private Button quitButton;
    private Button playAgainButton;
    private Button MainMenuButton;
    private Button CloseSettingsButton;
    private VisualElement settingsParent;



    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        playButton = root.Q<Button>("Play");
        settingsButton = root.Q<Button>("Settings");
        quitButton = root.Q<Button>("Quit");
        CloseSettingsButton = root.Q<Button>("Close");
        settingsParent = root.Q<VisualElement>("SettingsPanel");

        playAgainButton = root.Q<Button>("PlayAgain");
        MainMenuButton = root.Q<Button>("MainMenu");

        playButton.RegisterCallback<ClickEvent>(OnPlayButtonClicked);
        playAgainButton.RegisterCallback<ClickEvent>(OnPlayAgainButtonClicked);
        MainMenuButton.RegisterCallback<ClickEvent>(OnMainMenuButtonClicked);
        settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
        quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClicked);
        CloseSettingsButton.RegisterCallback<ClickEvent>(OnCloseSettingsButtonClicked);
        settingsParent.style.display = DisplayStyle.None;
    }

    void OnPlayAgainButtonClicked(ClickEvent clickEvent)
    {
        GameManager.Instance.Audio.PlayButtonSound();
        SceneManager.LoadScene("level1");
    }
    void OnMainMenuButtonClicked(ClickEvent clickEvent)
    {
        GameManager.Instance.Audio.PlayButtonSound();
        SceneManager.LoadScene("Menu");
    }
    void OnPlayButtonClicked(ClickEvent clickEvent)
    {
        GameManager.Instance.Audio.PlayButtonSound();
        SceneManager.LoadScene("level1");
    }
    void OnSettingsButtonClicked(ClickEvent clickEvent)
    {
        GameManager.Instance.Audio.PlayButtonSound();
        settingsParent.style.display = DisplayStyle.Flex;
    }
    void OnQuitButtonClicked(ClickEvent clickEvent)
    {
        GameManager.Instance.Audio.PlayButtonSound();
        Application.Quit();
    }
    void OnCloseSettingsButtonClicked(ClickEvent clickEvent)
    {
        GameManager.Instance.Audio.PlayButtonSound();
        settingsParent.style.display = DisplayStyle.None;
    }
}
