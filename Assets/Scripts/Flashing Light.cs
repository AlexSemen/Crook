using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FlashingLight : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAlarm(bool isAlarm)
    {
        _animator.SetBool(AnimatorAlarm.Params.Alarm, isAlarm);
    }
}
