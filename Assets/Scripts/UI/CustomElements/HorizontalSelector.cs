using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HorizontalSelector : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private GameObject _optionMold;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private ScrollRect _scrollRect;

    private bool _startSnapMovement = false;
    private bool _shouldAdjust = false;
    private float _itemPosVariance;
    private float _targetPos;
    private float _snappingTime = 0.15f;
    private float _currSnappingProgress = 0f;
    private float _initialSnappingPos;
    
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
        if (_startSnapMovement)
        {
            _currSnappingProgress += Time.deltaTime / _snappingTime;
            _scrollRect.horizontalNormalizedPosition = Mathf.Lerp(_initialSnappingPos, _targetPos, _currSnappingProgress);
            if (_currSnappingProgress >= 1f)
            {
                _scrollRect.horizontalNormalizedPosition = _targetPos;
                _startSnapMovement = false;
            }
        }
        if (_shouldAdjust)
        {
            if(_scrollRect.velocity.magnitude <= _itemPosVariance)
            {
                _startSnapMovement = true;
                _shouldAdjust = false;
                float scrollTotalVariance = _optionsPanel.GetComponent<RectTransform>().sizeDelta.x - GetComponent<RectTransform>().rect.width;
                float currentPos = _scrollRect.horizontalNormalizedPosition * scrollTotalVariance;
                float currentVelocity = _scrollRect.velocity.magnitude;
                float modCurrPos = currentPos % _itemPosVariance;
                _initialSnappingPos = _scrollRect.horizontalNormalizedPosition;
                Debug.LogFormat("scrollRect reached stop speed: scrollTotalVariance = {0}, currentPos = {1}, currentVelocity = {2}, modCurrPos = {3}",
                    scrollTotalVariance, currentPos, currentVelocity, modCurrPos);
                _scrollRect.velocity = new Vector2();
                _currSnappingProgress = 0f;
                if(modCurrPos > (_itemPosVariance / 2))
                {
                    _targetPos = _scrollRect.horizontalNormalizedPosition + (_itemPosVariance - modCurrPos) / scrollTotalVariance;
                }
                else
                {
                    _targetPos = _scrollRect.horizontalNormalizedPosition - modCurrPos / scrollTotalVariance;
                }
            }
            // TODO: Add the snap during the scrolling;
        }
    }

    public void AddOption(GameObject content)
    {
        GameObject newOption = Instantiate(_optionMold, _optionsPanel.transform);
        newOption.GetComponent<AutoShrinkSeelctorOption>()?.Initialize(_scrollRect, content);

    }

    private void UpdateScrollPosition(Vector2 pos)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _shouldAdjust = true;
        _itemPosVariance = (_optionsPanel.GetComponent<RectTransform>().sizeDelta.x - GetComponent<RectTransform>().rect.width) /
            (_optionsPanel.transform.childCount - 1);

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startSnapMovement = false;
    }
}
