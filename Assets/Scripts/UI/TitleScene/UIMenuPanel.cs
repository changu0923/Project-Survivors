using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuPanel : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button optionButton;
    [SerializeField] Button exitButton;

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        startButton.onClick.RemoveAllListeners();
        optionButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(OnStartButtonClick);
        optionButton.onClick.AddListener(OnOptionButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnStartButtonClick()
    {
        AudioManager.Instance.PlayUIButton();
        SceneManager.LoadScene("GameScene");
    }

    private void OnOptionButtonClick()
    {
        AudioManager.Instance.PlayUIButton();
        // TOOD : Options
    }

    private void OnExitButtonClick()
    {
        AudioManager.Instance.PlayUIButton();
        Application.Quit();
    }

}
