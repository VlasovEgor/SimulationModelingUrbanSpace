
public class BuildingSelectionPresenter
{
    private readonly BildingSelectionView _bildingSelectionView;

    private Building _building;
    private StructureCreator _structureCreator;
    
    public BuildingSelectionPresenter(Building building, BildingSelectionView bildingSelectionView)
    {
        _bildingSelectionView = bildingSelectionView;
        _building = building;
    }

    public void Construct(StructureCreator structureCreator)
    {
        _structureCreator = structureCreator;
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
        _structureCreator.Create(_building);
    }
}
