using System;
using System.Collections;
using UnityEngine;

public class QTEController : MonoBehaviour
{
    public event EventHandler<QTEState> OnQTEAction;
    public event EventHandler<QTEState> OnQTEFinshed;

    public float GetTime { get { return 1 - timer / qteCycleTime; } }
    public int GetQTECycle { get { return maxCycleCount; } }
    public int CurrentCycle
    {
        get { return currentCycleCount; }
        set
        {
            if (value == maxCycleCount) { CheckGameState(); }
            currentCycleCount = value;
        }
    }

    [Header("Settings")]
    [Range(1, 5)]
    [Tooltip("How many times does a player have to do the QTE")]
    [SerializeField] private int maxCycleCount;
    [Range(2, 5)]
    [Tooltip("How much time does the player have to finish a cycle")]
    [SerializeField] private int qteCycleTime;

    private QTEPresenter presenter;
    private float timer;
    private int qtePlayerSucceedCount;
    private int currentCycleCount;

    private const string KEY_ID = "QTEAction";

    private void Awake()
    {
        presenter = GetComponent<QTEPresenter>();
    }

    private void Start()
    {
        QTEManager.Instance.OnQTEStart += Manager_OnQTEStart;
        QTEManager.Instance.OnQTEStop += Manager_OnQTEStop;
    }

    private void OnDestroy()
    {
        QTEManager.Instance.OnQTEStart -= Manager_OnQTEStart;
        QTEManager.Instance.OnQTEStop -= Manager_OnQTEStop;
    }

    private void Manager_OnQTEStop()
    {
        StopAllCoroutines();
    }

    private void Manager_OnQTEStart()
    {
        ResetTimer();
        ResetCycle();
        StartCoroutine(QTE());
    }

    private IEnumerator QTE()
    {
        presenter.SetTargetSizeAndPosition();
        while (currentCycleCount != maxCycleCount)
        {
            presenter.PingPongQTE();
            if (Input.GetButtonDown(KEY_ID) || timer <= 0)
            {
                if (presenter.PlayerHitTarget())
                    qtePlayerSucceedCount++;
                OnQTEAction?.Invoke(this, presenter.PlayerHitTarget() ? QTEState.Succeeded : QTEState.Failed);
                StartNextCycle();
            }
            timer -= Time.deltaTime;
            yield return null;
        }
    }

    private void ResetTimer()
    {
        timer = qteCycleTime;
    }

    private void ResetCycle()
    {
        currentCycleCount = 0;
        qtePlayerSucceedCount = 0;
    }

    private void StartNextCycle()
    {
        CurrentCycle++;
        presenter.SetTargetSizeAndPosition();
        ResetTimer();
    }

    private void CheckGameState()
    {
        OnQTEFinshed?.Invoke(this, PlayerPassedQTE() ? QTEState.Succeeded : QTEState.Failed);
    }

    private bool PlayerPassedQTE()
    {
        return qtePlayerSucceedCount > maxCycleCount / 2;
    }
}
