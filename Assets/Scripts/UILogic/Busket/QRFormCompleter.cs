using UnityEngine;
using Zenject;

public class QRFormCompleter : MonoBehaviour
{
    [SerializeField] private string _notificationPaymentComplete = "Спасибо за ваш заказ! Оплата подтвердится в течении суток";

    [Inject] private NotificationDisplayer _notificationDisplayer;

    public void OnPaymentComplete()
    {
        _notificationDisplayer.ShowNotificationSafety(_notificationPaymentComplete);
        gameObject.SetActive(false);
    }
}
