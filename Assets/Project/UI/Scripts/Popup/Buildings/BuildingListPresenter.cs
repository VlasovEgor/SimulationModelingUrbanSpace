using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuildingListPresenter : MonoBehaviour
{
    [SerializeField] private BildingSelectionView _selectionViewPrefab;

    [SerializeField] private Transform _viewContainer;

    [Inject] private BuildingsManager _buildingsManager;

    [Inject] private StructureCreator _structureCreator;

    private List<BildingSelectionView> _activeView = new();
    private List<BuildingSelectionPresenter> _activePresenter = new();

    public BildingSelectionView BildingSelectionView
    {
        get => default;
        set
        {
        }
    }

    public BuildingSelectionPresenter BuildingSelectionPresenter
    {
        get => default;
        set
        {
        }
    }

    public BuildingsManager BuildingsManager
    {
        get => default;
        set
        {
        }
    }

    public void Show(string buildingsType)
    {
        Building[] buildings = _buildingsManager.GetAllBuildings(buildingsType);

        foreach (Building building in buildings) 
        {
            BildingSelectionView view = Instantiate(_selectionViewPrefab, _viewContainer);
            _activeView.Add(view);

            BuildingSelectionPresenter presenter = new BuildingSelectionPresenter(building, view);
            presenter.Construct(_structureCreator);
            _activePresenter.Add(presenter);
        }

        foreach (var presenter in _activePresenter)
        {
            presenter.Start();
        }
    }

    public void Hide()
    {
        foreach (var presenter in _activePresenter)
        {
            presenter.Stop();
        }

        foreach (var view in _activeView)
        {
           Destroy(view.gameObject);
        }

        _activeView.Clear();
        _activePresenter.Clear();
    }
}
