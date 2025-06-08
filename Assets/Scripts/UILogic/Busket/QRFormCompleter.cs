using UnityEngine;
using Zenject;

public class QRFormCompleter : MonoBehaviour
{
    [SerializeField] private string _notificationPaymentComplete = "������� �� ��� �����! ������ ������������ � ������� �����";

    [Inject] private NotificationDisplayer _notificationDisplayer;

    public void OnPaymentComplete()
    {
        _notificationDisplayer.ShowNotificationSafety(_notificationPaymentComplete);
        gameObject.SetActive(false);
    }
}
