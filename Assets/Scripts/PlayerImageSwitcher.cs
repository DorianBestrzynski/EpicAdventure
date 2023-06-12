using UnityEngine;
using UnityEngine.UI;

public class PlayerImageSwitcher : MonoBehaviour
{
    public Image playerImage;

    public Sprite image1;
    public Sprite image2;
    public bool isKnight;

    private void Start()
    {
        UpdatePlayerImage();
    }


    private void UpdatePlayerImage()
    {
        if (StaticVariables.hasUnlockedBetterKnight)
        {
            playerImage.sprite = image1;
            if (isKnight)
            {
                playerImage.rectTransform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                playerImage.rectTransform.localScale = new Vector3(10f, 10f, 1f);
                playerImage.rectTransform.anchoredPosition += new Vector2(0f, 344f);
            }
        }
        else
        {
            playerImage.sprite = image2;
        }
    }
}