using System;
using UnityEngine;
using UnityEngine.UI;
using UnityRandom = UnityEngine.Random;

[DisallowMultipleComponent]
public class QTEPresenter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Image staticQTEBar;
    [SerializeField] private Image targetQTEBar;
    [SerializeField] private Image player;
    [SerializeField] private GameObject streakImageHolder;

    [Header("Settings")]
    [SerializeField] private float minTargetSize;
    [SerializeField] private float maxTargetSize;

    [Range(0,1)]
    [Tooltip("How fast does the notch move left to right")]
    [SerializeField] private float movementSpeed;

    private float targetHeight;
    private float qteBarWidth;

    private float qtebarDivded;
    private float qtetargetDivded;

    private void Start()
    {
        InitVisuals();
        QTEManager.Instance.OnQTEPrep += Instance_OnQTEPrep;
        QTEManager.Instance.OnQTEStop += Instance_OnQTEStop;
    }

    private void OnDestroy()
    {
        QTEManager.Instance.OnQTEPrep -= Instance_OnQTEPrep;
        QTEManager.Instance.OnQTEStop -= Instance_OnQTEStop;
    }

    private void Instance_OnQTEPrep()
    {
        ShowAndHideQTEVisual(true);
    }

    private void Instance_OnQTEStop()
    {
        ShowAndHideQTEVisual(false);
        ResetStreakVisuals();
    }

    /// <summary>
    /// Set the QTE target size and position
    /// </summary>
    public void SetTargetSizeAndPosition()
    {
        int targetWidth = (int)UnityRandom.Range(minTargetSize, maxTargetSize);
        targetQTEBar.rectTransform.sizeDelta = new(targetWidth, targetHeight);
        int targetPosition = (int)UnityRandom.Range(-qtebarDivded, qtebarDivded);
        targetQTEBar.rectTransform.localPosition = new Vector3(targetPosition, 0, 0);
        qtetargetDivded = targetQTEBar.rectTransform.sizeDelta.x / 2;
    }

    /// <summary>
    /// Ping pong the QTE player
    /// </summary>
    public void PingPongQTE()
    {
        var positionPercentage = Mathf.PingPong(movementSpeed * Time.time, 1);

        player.rectTransform.anchorMin = new Vector2(positionPercentage, 0.5f);
        player.rectTransform.anchorMax = new Vector2(positionPercentage, 0.5f);
    }

    /// <summary>
    /// Did the players visual hit within the target visual
    /// </summary>
    public bool PlayerHitTarget()
    {
        return Vector3.Distance(player.rectTransform.localPosition, targetQTEBar.rectTransform.localPosition) <= qtetargetDivded;
    }

    /// <summary>
    /// Show and hide the visuals of the QTE
    /// </summary>
    /// <param name="set"></param>
    public void ShowAndHideQTEVisual(bool set)
    {
        mainCanvas.enabled = set;
    }

    /// <summary>
    /// Initilize visuals
    /// </summary>
    private void InitVisuals()
    {
        targetHeight = targetQTEBar.rectTransform.sizeDelta.y;
        qteBarWidth = staticQTEBar.rectTransform.sizeDelta.x;
        qtebarDivded = qteBarWidth / 2;
    }

    /// <summary>
    /// Reset the streak visuals
    /// </summary>
    private void ResetStreakVisuals()
    {
        for (int i = 0; i < streakImageHolder.transform.childCount; i++)
        {
            streakImageHolder.transform.GetChild(i).GetComponent<QTEStreakImageSetter>().ResetImage();
        }
    }
}
