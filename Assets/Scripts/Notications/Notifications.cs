using UnityEngine;
using Unity.Notifications.Android;

public class Notifications : MonoBehaviour
{
    
    [SerializeField]
    public string[] notificationList;

    void Start()
    {
       var channel = new AndroidNotificationChannel()
        {
            Id = "channel_1",
            Name = "Kilawa Notications",
            Importance = Importance.Default,
            Description = "Kilawa notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification();
        notification.Title = "Kilawa's adventure";
        notification.Text = notificationList[Random.Range(0, notificationList.Length)];
        notification.FireTime = System.DateTime.Now.AddHours(6);

        AndroidNotificationCenter.SendNotification(notification, "channel_1");
    }
}
