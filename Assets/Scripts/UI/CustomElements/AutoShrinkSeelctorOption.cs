﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoShrinkSeelctorOption : MonoBehaviour
{
    [SerializeField] private RectTransform _shrinkingPanel = null;
    [SerializeField] private ScrollRect _selector = null;
    [SerializeField] private float _shrinkedScale = .5f;
    [SerializeField] private float _distanceTrashold = 300f;

    void Start()
    {
        _selector.onValueChanged.AddListener((Vector2) => updateScale());
        updateScale();
    }

    public void Initialize(ScrollRect selector, GameObject content)
    {
        _selector = selector;
        content.transform.SetParent(this.transform);
        content.transform.localPosition = new Vector3();
        _shrinkingPanel = content.GetComponent<RectTransform>();
    }

    private void updateScale()
    {
        float currScale = _shrinkedScale + (1f - _shrinkedScale) *
            (1f - Mathf.Min( Mathf.Abs(transform.position.x - _selector.transform.position.x), 
            _distanceTrashold) / _distanceTrashold);
        _shrinkingPanel.localScale = new Vector3(currScale, currScale, currScale);
    }
}
