using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Crook : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRunningAway;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    public bool _isRunningAway;

    private void Awake()
    {
        _isRunningAway = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_isRunningAway == false)
        {
            _animator.SetFloat(AnimatorCrookController.Params.Speed, _speed);
            _rigidbody2D.velocity = transform.right * _speed;
        }
        else
        {
            _animator.SetFloat(AnimatorCrookController.Params.Speed, _speedRunningAway);
            _rigidbody2D.velocity = transform.right * -_speedRunningAway;
        }
    }

    public void RunningAway()
    {
        if (_isRunningAway == false)
        {
            _spriteRenderer.flipX = true;
            _isRunningAway = true;
        }
    }
}