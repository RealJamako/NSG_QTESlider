using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class QTEStreakImageSetter : MonoBehaviour
{
    [SerializeField] private QTEImageSO image;
    private Image streakImage;

    private void Awake()
    {
        streakImage = GetComponent<Image>();
    }

    private void OnDisable()
    {
        ResetImage();
    }

    public void SetImage(QTEState state)
    {
        switch (state)
        {
            case QTEState.Succeeded:
                streakImage.sprite = image.susuccessImage;
                break;
            case QTEState.Failed:
                streakImage.sprite = image.failedImage;
                break;
        }
    }

    public void ResetImage()
    {
        streakImage.sprite = image.blankImage;
    }
}
