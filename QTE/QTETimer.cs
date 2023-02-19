using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class QTETimer : MonoBehaviour
{
    [SerializeField] private QTEController controller;
    [SerializeField] private Image timerImage;

    private void Update()
    {
        timerImage.fillAmount = controller.GetTime;
    }
}
