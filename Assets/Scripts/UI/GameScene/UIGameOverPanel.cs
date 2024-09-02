using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverPanel : MonoBehaviour
{
    private Image panelImage;
    [SerializeField] Image gameoverImage;
    [SerializeField] Image gameWinImage;
    [SerializeField] Button checkButton;
    private bool isWin;

    private void OnEnable()
    {
        panelImage = GetComponent<Image>();        
        isWin = GameManager.Instance.IsWin;
        StartCoroutine(AnimationFadeIn());

        checkButton.onClick.RemoveAllListeners();
        checkButton.onClick.AddListener(OnCheckButtonClicked);
    }

    private void OnCheckButtonClicked()
    {
        GameManager.Instance.ChangeScene("TitleScene");
    }

    IEnumerator AnimationFadeIn()
    {
        Image subtitleImage = isWin ? gameWinImage : gameoverImage;
        subtitleImage.gameObject.SetActive(true);

        Color panelInitColor = panelImage.color;
        Color subtitleInitColor = subtitleImage.color;
        Color buttonInitColor = checkButton.image.color; 

        panelImage.color = new Color(panelInitColor.r, panelInitColor.g, panelInitColor.b, 0);
        subtitleImage.color = new Color(subtitleInitColor.r, subtitleInitColor.g, subtitleInitColor.b, 0);
        checkButton.image.color = new Color(buttonInitColor.r, buttonInitColor.g, buttonInitColor.b, 0);

        float duration = 2.5f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration);

            panelImage.color = new Color(panelInitColor.r, panelInitColor.g, panelInitColor.b, alpha * panelInitColor.a);
            subtitleImage.color = new Color(subtitleInitColor.r, subtitleInitColor.g, subtitleInitColor.b, alpha * subtitleInitColor.a);
            checkButton.image.color = new Color(buttonInitColor.r, buttonInitColor.g, buttonInitColor.b, alpha * buttonInitColor.a);

            yield return null;
        }
        panelImage.color = panelInitColor;
        subtitleImage.color = subtitleInitColor;
        checkButton.image.color = buttonInitColor;
    }
}
