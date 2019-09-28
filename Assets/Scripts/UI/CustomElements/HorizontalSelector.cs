using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HorizontalSelector : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private GameObject _optionMold;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private ScrollRect _scrollRect;

    private bool _shouldAdjust = false;
    
    void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _scrollRect.onValueChanged.AddListener(UpdateScrollPosition);
    }

    void Update()
    {
        if(_shouldAdjust && _scrollRect.velocity.magnitude <= 0)
        {
            Debug.Log(_scrollRect.horizontalNormalizedPosition);
            _shouldAdjust = false;
        }
    }

    public void AddUnit(Unit addedUnit)
    {
        GameObject unitOpt = Instantiate(_optionMold, _optionsPanel.transform);
        unitOpt.GetComponent<AutoShrinkSeelctorOption>()?.Initalize(_scrollRect);

        // TODO: Pass down the unit data
    }

    private void UpdateScrollPosition(Vector2 pos)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _shouldAdjust = true;
    }
}
