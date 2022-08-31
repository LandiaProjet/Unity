using UnityEngine;
using Unity.Notifications.Android;

public class Notifications : MonoBehaviour
{
    
    [SerializeField]
    public string[] notificationList;

    void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

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
        //TODO : changer les valeurs ici (1-2) -> (6-24)
        notification.FireTime = System.DateTime.Now.AddHours(Random.Range(1, 2));

        var id = AndroidNotificationCenter.SendNotification(notification, "channel_1");

        if(AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled){
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_1");
        }
    
    
    }


}
