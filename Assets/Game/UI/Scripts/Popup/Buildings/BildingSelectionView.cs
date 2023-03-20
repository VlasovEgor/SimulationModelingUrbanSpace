using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BildingSelectionView : MonoBehaviour
{
    public SelectButton BuildingButton
    {
        get { return _buildingButton; }
    }

    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private SelectButton _buildingButton;
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
