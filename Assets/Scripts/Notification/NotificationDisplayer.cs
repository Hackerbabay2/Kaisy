using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class NotificationDisplayer : MonoBehaviour
{
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private GameObject _notificationPanel;
    [SerializeField] private TMP_Text _notificationText;
    [SerializeField] private float _notificationSpeed;
    [SerializeField] private float _notificationDuratoin;

    private Vector2 _startPosition;
    private Coroutine _notificationCoroutine;

    private void Awake()
    {
        _startPosition = _notificationPanel.transform.position;
    }

    public void ShowNotificationSafety(string title)
    {
        if (_notificationCoroutine != null)
        {
            StopCoroutine(_notificationCoroutine);
            _notificationCoroutine = null;
        }

        _notificationPanel.transform.position = _startPosition;
        _notificationCoroutine = StartCoroutine(ShowNotification(title));
    }

    public IEnumerator ShowNotification(string title)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_notificationDuratoin);
        _notificationText.text = title;
        _notificationPanel.transform.DOMove(_targetPoint.position, _notificationSpeed);
        yield return waitForSeconds;
        _notificationPanel.transform.DOMove(_startPosition, _notificationSpeed);
    }
}