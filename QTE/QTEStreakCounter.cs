using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
public class QTEStreakCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject streakHolder;
    [SerializeField] private GameObject qteImagePrefab;
    [SerializeField] private QTEAudioSO qteAudio;

    private AudioSource audiosource;
    private QTEController qteController;
    private List<QTEStreakImageSetter> streakImageSetters;

    private void Awake()
    {
        streakImageSetters = new();
        audiosource = GetComponent<AudioSource>();
        qteController = GetComponent<QTEController>();
    }

    private void Start()
    {
        SpawnStreakPrefabs();
        qteController.OnQTEAction += QteController_OnQTEAction;
    }

    private void OnDestroy()
    {
        qteController.OnQTEAction -= QteController_OnQTEAction;
    }

    private void QteController_OnQTEAction(object sender, QTEState e)
    {
        streakImageSetters[qteController.CurrentCycle].SetImage(e);
        switch (e)
        {
            case QTEState.Succeeded:
                audiosource.PlayOneShot(qteAudio.successAudio);
                break;
            case QTEState.Failed:
                audiosource.PlayOneShot(qteAudio.failedAudio);
                break;
        }
    }

    /// <summary>
    /// This method is used to spawn the set amount of images 
    /// </summary>
    public void SpawnStreakPrefabs()
    {
        for (int i = 0; i < qteController.GetQTECycle; i++)
        {
            var spawned = Instantiate(qteImagePrefab, streakHolder.transform);
            streakImageSetters.Add(spawned.GetComponent<QTEStreakImageSetter>());
        }
    }
}
