//класс отвечающий за перемещение пользователй
using System;
using UnityEngine;

public class MoveInDirectionEngine : MonoBehaviour
{
    public event Action OnStartMove;
    public event Action OnStopMove;

    [SerializeField] private BoolBehavior _movingBoolBehavior;

    [SerializeField] private BoolBehavior _canBoost;
    [SerializeField] private IntBehaviour _boost;

    public bool IsMoving
    {
        get { return _direction != Vector3.zero; }
    }

    public Vector3 Direction
    {
        get { return _direction; }
    }

    private Vector3 _direction;

    private void Update()
    {
        UpdateMove();
        MotionCheck();
        Boost();
    }

    public void Move(Vector3 direction)
    {
        _direction = direction;

        if(_direction != Vector3.zero)
        {
            StartMove();
        }   
    }

    private void Boost()
    {
        if (_canBoost.Value == true)
        {
            _direction *= _boost.Value;
        }
    }

    private void StartMove()
    {
        OnStartMove?.Invoke();
    }

    private void UpdateMove()
    {
        if (_direction == Vector3.zero)
        {
            StopMove();
        }
    }

    private void StopMove()
    {
        OnStopMove?.Invoke();
    }


    private void MotionCheck()
    {
        if(IsMoving==true)
        {
            _movingBoolBehavior.AssignTrue();
        }
        else
        {
            _movingBoolBehavior.AssignFalse();
        }
    }
}