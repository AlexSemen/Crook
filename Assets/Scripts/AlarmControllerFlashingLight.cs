using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmControllerFlashingLight : MonoBehaviour
{
    [SerializeField] private FlashingLight _flashingLight;

    public void SetAlarm(bool alarm)
    {
        _flashingLight.SetAlarm(alarm);
    }
}
