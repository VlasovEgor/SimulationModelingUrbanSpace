using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WindowGraph : MonoBehaviour
{
    private static WindowGraph _instance;

    [SerializeField] private Sprite _dotSprite;

    [Space]
    [SerializeField] private RectTransform _graphContainer;
    [SerializeField] private RectTransform _labelTemplateX;
    [SerializeField] private RectTransform _labelTemplateY;

    [Space]
    [SerializeField] private RectTransform dashContainer;
    [SerializeField] private RectTransform _dashTemplateX;
    [SerializeField] private RectTransform _dashTemplateY;

    [Space]
    [SerializeField] private GameObject _tooltipGameObject;
    [SerializeField] private TextMeshProUGUI _tooltipGameObjectText;
    [SerializeField] private GameObject _tooltipGameObjectBackground;

    [Inject] private StatisticsCollector _statisticsCollector;

    private List<GameObject> _gameObjectList = new();
    private List<RectTransform> _labelsList = new();

    private List<int> _valueList;
    private IGraphVisual _graphVisual;
    private int _maxVisibleValueAmount;
    private Func<int, string> _getAxisLabelX;
    private Func<float, string> _getAxisLabelY;

    private IGraphVisual _lineGraphVisual;
    private IGraphVisual _barChartVisual;

    private void Awake()
    {
        _instance = this;

        _lineGraphVisual = new LineGraphVisual(_graphContainer, _dotSprite, Color.red, Color.white);
        _barChartVisual = new BarChartVisual(_graphContainer, Color.white, 0.8f);
        _graphVisual = _lineGraphVisual;
    }

    public static void ShowTooltip_Static(string tooltipText, Vector2 anchoredPosition)
    {
        _instance.ShowTooltip(tooltipText, new Vector2( anchoredPosition.x -850, anchoredPosition.y -370));
    }

    private void ShowTooltip(string tooltipText, Vector2 anchoredPosition)
    {
        _tooltipGameObject.SetActive(true);
        _tooltipGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;

        _tooltipGameObjectText.text = tooltipText;

        _tooltipGameObject.transform.SetAsLastSibling();
        _tooltipGameObjectText.transform.SetAsLastSibling();
    }

    public static void HideTooltip_Static()
    {
        _instance.HideTooltip();
    }

    private void HideTooltip()
    {
        _tooltipGameObject.SetActive(false);
    }

    public void SetBarChart()
    {
        SetGraphVisual(_barChartVisual);
    }

    public void SetLineGraph()
    {
        SetGraphVisual(_lineGraphVisual);
    }

    private void SetGraphVisual(IGraphVisual graphVisual)
    {
        ShowGraph(_valueList, graphVisual, _maxVisibleValueAmount, _getAxisLabelX, _getAxisLabelY);
    }

    public void IncreaseVisibleAmount()
    {
        ShowGraph(_valueList, _graphVisual, _maxVisibleValueAmount + 1, _getAxisLabelX, _getAxisLabelY);
    }

    public void DecreaseVisibleAmount()
    {
        ShowGraph(_valueList, _graphVisual, _maxVisibleValueAmount - 1, _getAxisLabelX, _getAxisLabelY);
    }

  
    public void ShowPopulationSize()
    {
        _valueList = _statisticsCollector.GetPoulationSize();
        ShowGraph(_valueList, _graphVisual, _maxVisibleValueAmount, _getAxisLabelX, _getAxisLabelY);
    }

    public void ShowLevelHappiness()
    {
        _valueList = _statisticsCollector.GetLevelHappiness();
        ShowGraph(_valueList, _graphVisual, _maxVisibleValueAmount, _getAxisLabelX, _getAxisLabelY);
    }

    public void ShowNumberResidentialBuildings()
    {  
        _valueList = _statisticsCollector.GetNumberResidentialBuildings();
        ShowGraph(_valueList, _graphVisual, _maxVisibleValueAmount, _getAxisLabelX, _getAxisLabelY);
    }

    public void ShowNumberBuildingsTypeWork()
    {  
        _valueList = _statisticsCollector.GetNumberBuildingsTypeWork();  
        ShowGraph(_valueList, _graphVisual, _maxVisibleValueAmount, _getAxisLabelX, _getAxisLabelY);
    }

    public void ShowNumberBuildingsTypeFood()
    {  
        _valueList = _statisticsCollector.GetBuildingsTypeFood();
        ShowGraph(_valueList, _graphVisual, _maxVisibleValueAmount, _getAxisLabelX, _getAxisLabelY);
    }

    public void ShowNumberBuildingsTypeSport()
    {   
        _valueList = _statisticsCollector.GetBuildingsTypeSport();
        ShowGraph(_valueList, _graphVisual, _maxVisibleValueAmount, _getAxisLabelX, _getAxisLabelY);
    }

    public void ShowNumberBuildingsTypeHealth()
    {   
        _valueList = _statisticsCollector.GetBuildingsTypeHealth();
        ShowGraph(_valueList, _graphVisual, _maxVisibleValueAmount, _getAxisLabelX, _getAxisLabelY);
    }

    public void ShowNumberBuildingsTypeRelax()
    {   
        _valueList = _statisticsCollector.GetBuildingsTypeRelax();
        ShowGraph(_valueList, _graphVisual, _maxVisibleValueAmount, _getAxisLabelX, _getAxisLabelY);
    }

    public void ShowNumberVacancies()
    {   
        _valueList = _statisticsCollector.GetNumberVacancies();
        ShowGraph(_valueList, _graphVisual, _maxVisibleValueAmount, _getAxisLabelX, _getAxisLabelY);
    }

    public void ShowNumberUnemployed()
    {   
        _valueList = _statisticsCollector.GetNumberUnemployed();
        ShowGraph(_valueList, _graphVisual, _maxVisibleValueAmount, _getAxisLabelX, _getAxisLabelY);
    }

    public void ShowPercentageCarSelection()
    {
        _valueList = _statisticsCollector.GetPercentageCarSelection();
        ShowGraph(_valueList, _graphVisual, _maxVisibleValueAmount, _getAxisLabelX, _getAxisLabelY);
    }

    private void ShowGraph(
        List<int> valueList,
        IGraphVisual graphVisual,
        int maxVisibleValueAmount = -1,
        Func<int, string> getAxisLabelX = null,
        Func<float, string> getAxisLabelY = null)
    {
        _valueList = valueList;
        _graphVisual = graphVisual;
        _getAxisLabelX = getAxisLabelX;
        _getAxisLabelY = getAxisLabelY;

        foreach (var label in _labelsList)
        {
            Destroy(label.gameObject);
        }
        _labelsList.Clear();

        if (getAxisLabelX == null)
        {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }
        if (maxVisibleValueAmount <= 0)
        {
            maxVisibleValueAmount = valueList.Count;
        }

        _maxVisibleValueAmount = maxVisibleValueAmount;

        foreach (GameObject gameObject in _gameObjectList)
        {
            Destroy(gameObject);
        }
        _gameObjectList.Clear();

        float graphWidth = _graphContainer.sizeDelta.x;
        float graphHeight = _graphContainer.sizeDelta.y;

        float yMaximum = valueList[0];
        float yMinimum = valueList[0];

        for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
        {
            int value = valueList[i];
            if (value > yMaximum)
            {
                yMaximum = value;
            }
            if (value < yMinimum)
            {
                yMinimum = value;
            }
        }

        float yDifference = yMaximum - yMinimum;

        if (yDifference <= 0)
        {
            yDifference = 5f;
        }

        yMaximum = yMaximum + (yDifference * 0.2f);
        yMinimum = yMinimum - (yDifference * 0.2f);

        yMinimum = 0f;

        float xSize = graphWidth / (maxVisibleValueAmount + 1);

        int xIndex = 0;

        for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
        {
            float xPosition = xSize + xIndex * xSize;
            float yPosition = ((valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;

            string tooltipText = getAxisLabelY(valueList[i]);
            _gameObjectList.AddRange(graphVisual.AddGraphVisual(new Vector2(xPosition, yPosition), xSize, tooltipText));

            RectTransform labelX = Instantiate(_labelTemplateX);
            labelX.SetParent(_graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -80f);
            labelX.GetComponent<TextMeshProUGUI>().text = getAxisLabelX(i);

            _labelsList.Add(labelX);


            RectTransform dashX = Instantiate(_dashTemplateX);
            dashX.SetParent(dashContainer, false);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPosition, 0f);
            _gameObjectList.Add(dashX.gameObject);

            xIndex++;
        }

        int separatorCount = 10;
        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(_labelTemplateY);
            labelY.SetParent(_graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-40f, normalizedValue * graphHeight - 30);
            labelY.GetComponent<TextMeshProUGUI>().text = getAxisLabelY(yMinimum + (normalizedValue * (yMaximum - yMinimum)));

            _labelsList.Add(labelY);

            RectTransform dashY = Instantiate(_dashTemplateY);
            dashY.SetParent(dashContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(-40f, normalizedValue * graphHeight - 20);
            _gameObjectList.Add(dashY.gameObject);
        }
    }

    private interface IGraphVisual
    {
        List<GameObject> AddGraphVisual(Vector2 graphPosition, float graphPositionWidth, string tooltipText);

    }

    private class BarChartVisual : IGraphVisual
    {
        private RectTransform _graphContainer;
        private Color _barColor;
        private float _barWidthMultiplier;

        public BarChartVisual(RectTransform graphContainer, Color barColor, float barWidthMultiplier)
        {
            _graphContainer = graphContainer;
            _barColor = barColor;
            _barWidthMultiplier = barWidthMultiplier;
        }

        public List<GameObject> AddGraphVisual(Vector2 graphPosition, float graphPositionWidth, string tooltipText)
        {

            GameObject barGameObject = CreateBar(graphPosition, graphPositionWidth);

            GraphUI barButtonUI = barGameObject.AddComponent<GraphUI>();

            barButtonUI.MouseOverOnceFunc += () =>
            {
                ShowTooltip_Static(tooltipText, graphPosition);
            };
            barButtonUI.MouseOutOnceFunc += () =>
            {
                HideTooltip_Static();
            };
            return new List<GameObject>() { barGameObject };
        }

        private GameObject CreateBar(Vector2 graphPosition, float barWidth)
        {
            GameObject gameObject = new GameObject("bar", typeof(Image));
            gameObject.transform.SetParent(_graphContainer, false);
            gameObject.GetComponent<Image>().color = _barColor;

            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
            rectTransform.sizeDelta = new Vector2(barWidth * _barWidthMultiplier, graphPosition.y);
            rectTransform.sizeDelta = new Vector2(barWidth * _barWidthMultiplier, graphPosition.y);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.pivot = new Vector2(.5f, 0f);

            return gameObject;
        }

    }

    private class LineGraphVisual : IGraphVisual
    {

        private RectTransform _graphContainer;
        private Sprite _dotSprite;
        private GameObject _lastDotGameObject;
        private Color _dotColor;
        private Color _dotConnectionColor;

        public LineGraphVisual(RectTransform graphContainer, Sprite dotSprite, Color dotColor, Color dotConnectionColor)
        {
            _graphContainer = graphContainer;
            _dotSprite = dotSprite;
            _dotColor = dotColor;
            _dotConnectionColor = dotConnectionColor;
            _lastDotGameObject = null;
        }
        public List<GameObject> AddGraphVisual(Vector2 graphPosition, float graphPositionWidth, string tooltipText)
        {


            List<GameObject> gameObjectList = new List<GameObject>();
            GameObject dotGameObject = CreateDot(graphPosition);

            GraphUI dotUI = dotGameObject.AddComponent<GraphUI>();

            dotUI.MouseOverOnceFunc += () =>
            {
                ShowTooltip_Static(tooltipText, graphPosition);
            };
            dotUI.MouseOutOnceFunc += () =>
            {
                HideTooltip_Static();
            };

            gameObjectList.Add(dotGameObject);
            if (_lastDotGameObject != null)
            {
                GameObject dotConnectionGameObject = CreateDotConnection(_lastDotGameObject.GetComponent<RectTransform>().anchoredPosition,
                    dotGameObject.GetComponent<RectTransform>().anchoredPosition);

                gameObjectList.Add(dotConnectionGameObject);
            }

            _lastDotGameObject = dotGameObject;
            return gameObjectList;
        }

        private GameObject CreateDot(Vector2 anchoredPosition)
        {
            GameObject gameObject = new GameObject("dot", typeof(Image));

            gameObject.transform.SetParent(_graphContainer, false);
            gameObject.GetComponent<Image>().sprite = _dotSprite;
            gameObject.GetComponent<Image>().color = _dotColor;

            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta = new Vector2(11, 11);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);

            return gameObject;
        }

        private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
        {
            GameObject gameObject = new GameObject("dotConnection", typeof(Image));
            gameObject.transform.SetParent(_graphContainer, false);
            gameObject.GetComponent<Image>().color = _dotConnectionColor;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            Vector2 dir = (dotPositionB - dotPositionA).normalized;
            float distance = Vector2.Distance(dotPositionA, dotPositionB);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.sizeDelta = new Vector2(distance, 3f);
            rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
            rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
            return gameObject;
        }

        public float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }
    }
}
