
public class BuildingSelectionPresenter
{
    private readonly BildingSelectionView _bildingSelectionView;

    private Building _building;

    private BuildingCreator _buildingCreator;

    public BuildingSelectionPresenter(Building building, BildingSelectionView bildingSelectionView)
    {
        _bildingSelectionView = bildingSelectionView;
        _building = building;
    }

    public void Construct(BuildingCreator buildingCreator)
    {
        _buildingCreator = buildingCreator;
    }

    public void Start()
    {
        _bildingSelectionView.BuildingButton.AddListener(OnButtonClicked);
        _bildingSelectionView.SetIcon(_building.Icon);
        _bildingSelectionView.SetTitle(_building.Title);
    }

    public void Stop()
    {
        _bildingSelectionView.BuildingButton.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _buildingCreator.Create(_building.BuildingPrefab);
    }
}
