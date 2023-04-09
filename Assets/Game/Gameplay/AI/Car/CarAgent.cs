using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CarAgent : MonoBehaviour
{   
    public event Action<Vector3> OnDrive;

    [SerializeField] private Transform _carTransform;

    [SerializeField] private List<Vector3> _path;
    [ShowInInspector,ReadOnly] private Vector3 _currentTargetPosition;

    [SerializeField] private float _arriveDistance = 0.3f;
    [SerializeField] private float _lastpointArriveDistance = 0.1f;

    private int _index;

    private bool _isMove;

    private void Start()
    {
        if (_path == null || _path.Count == 0)
        {
            _isMove= false;
        }
        else
        {
            _currentTargetPosition = _path[_index];
        }
    }

    private void Update()
    {
        CheckIsArrived();
        Drive();
    }

    public void SetPath(List<Vector3> path)
    {
        if (path.Count == 0)
        {
            Debug.Log("PRIEXALI_SetPath");
            return;
        }

        _path = path;
        _index = 0;
        _currentTargetPosition= _path[_index];
        _isMove = true;
    }

    private void CheckIsArrived()
    {
        if(_isMove == true) 
        {
            var distanceToCheck = _arriveDistance;

            if(_index == _path.Count-1)
            {
                distanceToCheck = _lastpointArriveDistance;
            }

            if(Vector3.Distance(_currentTargetPosition, _carTransform.position)< distanceToCheck)
            {
                SetNextTargetIndex();
            }
        }
    }

    private void SetNextTargetIndex()
    {
        _index++;
        if(_index >= _path.Count) 
        {
            _isMove= false;
            Debug.Log("PRIEXALI_SetNextTargetIndex");
        }
        else
        {
            _currentTargetPosition= _path[_index];
        }
    }

    private void Drive()
    {
        if(_isMove == false) 
        {
            OnDrive?.Invoke(Vector3.zero);
        }
        else
        {
            OnDrive?.Invoke(_currentTargetPosition);
        }
    }

}
