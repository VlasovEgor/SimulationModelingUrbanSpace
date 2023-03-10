using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BildingSelectionView : MonoBehaviour
{
    public BuildingButton BuildingButton
    {
        get { return _buildingButton; }
    }

    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private BuildingButton _buildingButton;
    [SerializeField] private Image _buttonBackground;

    public void SetTitle(string title)
    {
        _title.text = title;
    }

    public void SetIcon(Sprite icon)
    {
        _buttonBackground.sprite = icon;
    }

}
