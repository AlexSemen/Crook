using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmControllerSound : MonoBehaviour
{
    [SerializeField] private float _volumeChangeTime;

    private float _partVolumeChangeTime;
    private float _audioVolumeMin;
    private float _audioVolumeMax;
    private float _audioVolumeTarget;
    private AudioSource _audioSource;
    private Coroutine _fadeInJob;

    private void Awake()
    {
        _partVolumeChangeTime = 0;
        _audioVolumeMin = 0;
        _audioVolumeMax = 1;

        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _audioVolumeMin;
    }

    public void TurnAlarm(bool alarm)
    {
        if(_fadeInJob != null)
        {
            StopCoroutine(_fadeInJob);
        }

        if(alarm)
        {
            _audioVolumeTarget = _audioVolumeMax;
        }
        else
        {
            _audioVolumeTarget = _audioVolumeMin;
        }

        _fadeInJob = StartCoroutine(ChangingVolume(_audioSource.volume, _audioVolumeTarget, alarm));
    }

    private IEnumerator ChangingVolume(float initialVolume, float targetVolume, bool isWork)
    {
        if(isWork) 
        {
            _audioSource.Play();
        }

        _partVolumeChangeTime = initialVolume;

        while (_audioSource.volume != targetVolume)
        {
            _partVolumeChangeTime += Time.deltaTime;

            _audioSource.volume = Mathf.MoveTowards(initialVolume, targetVolume, _partVolumeChangeTime / _volumeChangeTime);

            yield return null;
        }

        if (isWork == false)
        {
            _audioSource.Stop();
        }
    }
}
