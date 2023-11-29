using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Recorder.OutputPath;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AlarmFlashingLight _controllerFlashingLight;
    [SerializeField] private AlarmSound _controllerSound;
    [SerializeField] private TriggerAlarm _trigersAlarm;

    private bool _isAlarm;
    private int _checkDelay;
    private int _crookRunningAwayDelay;

    private bool _isTargerAlarm
    {
        get
        {
            if (_trigersAlarm.IsAlarm)
            {
                return true;
            }

            return false;
        }
    }

    private void Awake()
    {
        _isAlarm = false;
        _checkDelay = 1;
        _crookRunningAwayDelay = 2;

        //_trigersAlarm = GetComponentsInChildren<TriggerAlarm>();
    }

    private void Start()
    {
        StartCoroutine(CheckAlarm());
    }

    private IEnumerator CheckAlarm()
    {
        bool isWork = true;
        var waitForDelay = new WaitForSeconds(_checkDelay);

        while (isWork)
        {
            if(_isAlarm != _isTargerAlarm)
            {
                _isAlarm = _isTargerAlarm;
                _controllerFlashingLight.SetAlarm(_isAlarm);
                _controllerSound.TurnAlarm(_isAlarm);

                if (_isAlarm == true)
                {
                    StartCoroutine(CrookRunningAway());
                }
            }

            yield return waitForDelay;
        }
    }

    private IEnumerator CrookRunningAway()
    {
        yield return new WaitForSeconds(_crookRunningAwayDelay);

        foreach (Crook crook in _trigersAlarm.Crooks)
        {
            crook.RunningAway();
        }
    }
}
