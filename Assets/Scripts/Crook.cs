using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(RequireComponent))]
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
            _animator.SetFloat("Speed", _speed);
            _rigidbody2D.velocity = transform.right * _speed;
        }
        else
        {
            _animator.SetFloat("Speed", _speedRunningAway);
            _rigidbody2D.velocity = transform.right * -_speedRunningAway;
        }
    }

    public void RunningAway()
    {
        _spriteRenderer.flipX = true;
        _isRunningAway = true;
    }
}
