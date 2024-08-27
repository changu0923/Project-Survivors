using UnityEngine;
using UnityEngine.UI;

public class UISkillElements : MonoBehaviour
{
    private Image uiImage;

    private void Awake()
    {
        uiImage = GetComponent<Image>();
    }

    public void SetImage(Sprite sprite)
    {
        uiImage.sprite = sprite;
    }
}
