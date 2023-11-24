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
    private bool isAlarm;

    private void Awake()
    {
        _partVolumeChangeTime = 0;
        _audioVolumeMin = 0;
        _audioVolumeMax = 1;
        isAlarm = false;
        
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _audioVolumeMin;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAlarm == false)
        {
            if (collision.TryGetComponent<Crook>(out Crook crook))
            {
                StartCoroutine(TurnOnAlarm(crook));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isAlarm == true)
        {
            if (collision.TryGetComponent<Crook>(out Crook crook))
            {
                StartCoroutine(TurnOffAlarm());
            }
        }
    }

    private IEnumerator TurnOnAlarm(Crook crook)
    {
        isAlarm = true;

        _flashingLight.SetAlarm(isAlarm);
        _audioSource.Play();

        StartCoroutine(ChangingVolume(_audioVolumeMin, _audioVolumeMax));

        yield return new WaitForSeconds(_volumeChangeTime); 
        crook.RunningAway();
    }

    private IEnumerator TurnOffAlarm()
    {
        isAlarm = false;
        
        StartCoroutine(ChangingVolume(_audioVolumeMax, _audioVolumeMin));
        
        yield return new WaitForSeconds(_volumeChangeTime);

        _flashingLight.SetAlarm(isAlarm);
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
