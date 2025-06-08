using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SnapScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _contentPanel;
    [SerializeField] private RectTransform _sampleListItem;
    [SerializeField] private HorizontalLayoutGroup _layoutGroup;
    [SerializeField] private float _snapSpeed = 1f;

    private int _currentItem;
    private bool _isDragging;

    private void Start()
    {
        _isDragging = false;
    }

    private void Update()
    {
        _currentItem = Mathf.RoundToInt(0 - _contentPanel.localPosition.x / (_sampleListItem.rect.width + _layoutGroup.spacing));

        if (_scrollRect.velocity.magnitude < 200 && !_isDragging)
        {
            _contentPanel.DOMove(_contentPanel.GetChild(_currentItem).transform.position, _snapSpeed);

            if (_contentPanel.localPosition.x == 0 - (_currentItem * (_sampleListItem.rect.width + _layoutGroup.spacing)))
            {
                _isDragging = true;
            }
        }

        if (_scrollRect.velocity.magnitude > 200)
        {
            _isDragging = false;
        }
    }
}