using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Recorder.OutputPath;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _volumeChangeTime;
    [SerializeField] private FlashingLight _flashingLight;

    private float _partVolumeChangeTime;
    private float _audioVolumeMin;
    private float _audioVolumeMax;
    private AudioSource _audioSource;
    private bool _isAlarm;
    private TrigerAlarm[] _trigersAlarm;
    private int _checkDelay;
    private Coroutine _coroutine;

    private bool _isTargerAlarm
    {
        get
        {
            foreach (var triger in _trigersAlarm)
            {
                if (triger.IsAlarm)
                {
                    return true;
                }
            }

            return false;
        }
    }

    private void Awake()
    {
        _partVolumeChangeTime = 0;
        _audioVolumeMin = 0;
        _audioVolumeMax = 1;
        _isAlarm = false;
        _checkDelay = 1;

        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _audioVolumeMin;

        _trigersAlarm = GetComponentsInChildren<TrigerAlarm>();
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
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                if (_isAlarm == false)
                {
                    _coroutine = StartCoroutine(TurnOnAlarm());
                }
                else
                {
                    _coroutine = StartCoroutine(TurnOffAlarm());
                }
            }

            yield return waitForDelay;
        }
    }

    private IEnumerator TurnOnAlarm()
    {
        _isAlarm = true;

        _flashingLight.SetAlarm(_isAlarm);
        _audioSource.Play();

        yield return StartCoroutine(ChangingVolume(_audioVolumeMin, _audioVolumeMax));

        foreach(Crook crook in TrigerAlarm.Crooks)
        {
            crook.RunningAway();
        }
    }

    private IEnumerator TurnOffAlarm()
    {
        _isAlarm = false;

        yield return StartCoroutine(ChangingVolume(_audioVolumeMax, _audioVolumeMin));
        
        _flashingLight.SetAlarm(_isAlarm);
        _audioSource.Stop();
    }

    private IEnumerator ChangingVolume(float initialVolume, float targetVolume)
    {
        _partVolumeChangeTime = initialVolume;

        while (_audioSource.volume != targetVolume)
        {
            _partVolumeChangeTime += Time.deltaTime;

            _audioSource.volume = Mathf.MoveTowards(initialVolume, targetVolume, _partVolumeChangeTime / _volumeChangeTime);

            yield return null;
        }
    }
}
