using Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SmartRoad : MonoBehaviour
{
    public event Action OnPedestrianCanWalk;

    [SerializeField] private SmartCrosswalks _smartCrosswalks;

    private bool _pedestrianWaiting = false;
    private bool _pedestrianWalking = false;

    private Queue<Agent> _trafficQueue = new Queue<Agent>();
    private Agent _currentCar;

    private void Start()
    {
        _smartCrosswalks.OnPedestrianEnter += SetPedestrianFlag;
        _smartCrosswalks.OnPedestrianExit+= SetPedestrianFlag;
    }

    private void OnDestroy()
    {
        _smartCrosswalks.OnPedestrianEnter += SetPedestrianFlag;
        _smartCrosswalks.OnPedestrianExit += SetPedestrianFlag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<UnityEntityProxy>().Get<IComponent_GetAgentType>().GetAgentType() == AgentType.CAR)
        {
            var car = other.GetComponent<UnityEntityProxy>().Get<IComponent_GetAgent>().GetAgent();
            if (car != null && car != _currentCar && car.IsThisLastPathIndex() == false)
            {
                _trafficQueue.Enqueue(car);
                car.SetMove(false);
            }
        }
    }

    private void Update()
    {
        if (_currentCar == null)
        {
            if (_trafficQueue.Count > 0 && _pedestrianWaiting == false && _pedestrianWalking == false)
            {
                _currentCar = _trafficQueue.Dequeue();
                _currentCar.SetMove(true);
            }
            else if (_pedestrianWalking || _pedestrianWaiting)
            {
                OnPedestrianCanWalk?.Invoke();
                _pedestrianWalking = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<UnityEntityProxy>().Get<IComponent_GetAgentType>().GetAgentType() == AgentType.CAR)
        {
            var car = other.GetComponent<UnityEntityProxy>().Get<IComponent_GetAgent>().GetAgent();
            if (car != null)
            {
                RemoveCar(car);
            }
        }
    }

    private void RemoveCar(Agent car)
    {
        if (car == _currentCar)
        {
            _currentCar = null;
        }
    }


    public void SetPedestrianFlag(bool val)
    {
        if (val == true)
        {
            _pedestrianWaiting = true;
        }
        else
        {
            _pedestrianWaiting = false;
            _pedestrianWalking = false;
        }
    }
}
