using UnityEngine;

[CreateAssetMenu(menuName = "NSG QTE Slider/QTEImageSO", fileName = "QTEImageSO")]
public class QTEImageSO : ScriptableObject
{
    [Header("Images")]
    public Sprite blankImage;
    public Sprite susuccessImage;
    public Sprite failedImage;
}
