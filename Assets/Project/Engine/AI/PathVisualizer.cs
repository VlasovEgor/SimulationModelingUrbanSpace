using Entities;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PathVisualizer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    [Inject] private ManipulationInput _manipulationInput;
    [Inject] private Camera _camera;

    private Agent _agent;

    private void Start()
    {
        _lineRenderer.positionCount = 0;
     //   _manipulationInput.LeftMouseButtonDown += TrySelectAgent;
    }

    private void OnDestroy()
    {
      //  _manipulationInput.LeftMouseButtonDown -= TrySelectAgent;
    }

    private void TrySelectAgent()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit) == true)
        {
            if (raycastHit.collider != null)
            {
                if (raycastHit.collider.gameObject.TryGetComponent(out IEntity entity) == true)
                {
                    if(entity.TryGet(out IComponent_GetAgent getAgnet) == true)
                    {
                        var agent = getAgnet.GetAgent();
                        var path = agent.GetPath();

                        ShowPath(path, agent);
                    }
                }
            }
        }
    }

    public void ShowPath(List<Vector3> path, Agent agent)
    {
        ResetPath(true);

        _lineRenderer.positionCount = path.Count;
        _agent = agent;

        _agent.ArrivedAtDestination += ResetPath;

        for (int i = 0; i < path.Count; i++)
        {
            _lineRenderer.SetPosition(i, path[i] + new Vector3(0, 1, 0));
        }
    }

    private void ResetPath(bool value)
    {
        _lineRenderer.positionCount = 0;
        _agent.ArrivedAtDestination -= ResetPath;
        _agent = null;
    }
}
