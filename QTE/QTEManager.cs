using System;
using UnityEngine;

[DisallowMultipleComponent]
public class QTEManager : MonoBehaviour
{
    public static QTEManager Instance { get; private set; }

    public bool IsActive { get; private set; }
    public QTEState State { get; private set; }

    public event Action OnQTEPrep;
    public event Action OnQTEStart;
    public event Action OnQTEStop;

    [Header("References")]
    public QTEController Controller;

    [Header("Settings")]
    [Range(0, 5)]
    [Tooltip("The delay for the QTE to start")]
    [SerializeField] private float startDelay;
    [Range(0,5)]
    [Tooltip("The delay for the QTE to stop")]
    [SerializeField] private float stopDelay;

    private void Awake() => InstanceCheck();

    private void Start()
    {
        Controller.OnQTEFinshed += Controller_OnQTEFinshed;
    }

    private void OnDestroy()
    {
        Controller.OnQTEFinshed -= Controller_OnQTEFinshed;
    }

    private void Controller_OnQTEFinshed(object sender, QTEState e)
    {
        State = e;
        Invoke(nameof(StopQTE), stopDelay);
    }

    public void StartQTE()
    {
        OnQTEPrep?.Invoke();
        IsActive = true;
        Invoke(nameof(QTEInvoke), startDelay);
    }

    public void StopQTE()
    {
        OnQTEStop?.Invoke();
        IsActive = false;
    }

    private void QTEInvoke() => OnQTEStart?.Invoke();

    /// <summary>
    /// Singleton Check 
    /// </summary>
    private void InstanceCheck()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance in the scene. Please clear any unnecessary instances.");
            return;
        }
        else
        {
            Instance = this;
        }
    }
}
