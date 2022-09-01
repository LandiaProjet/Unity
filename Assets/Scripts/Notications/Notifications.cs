
using UnityEngine;
using NotificationSamples;
using System;

public class Notifications : MonoBehaviour
{

    [SerializeField]
    public string[] notificationList;
    
    [SerializeField]
    protected GameNotificationsManager manager;

    public const string ChannelId = "game_channel0";

    public Notifications instance;

    private void Awake() {
        instance = this;
    }  


    void Start()
    {
        var c1 = new GameNotificationChannel(ChannelId, "Default Game Channel", "Generic notifications");

        manager.Initialize(c1);
        manager.CancelAllNotifications();

        DateTime deliveryTime = DateTime.Now.ToLocalTime().AddSeconds(UnityEngine.Random.Range(6, 24));
        SendNotification("Kilawa's adventure", notificationList[UnityEngine.Random.Range(0, notificationList.Length)], deliveryTime, channelId: Notifications.ChannelId);
    }

    public void SendNotification(string title, string body, DateTime deliveryTime, int? badgeNumber = null,
            bool reschedule = false, string channelId = null,
            string smallIcon = null, string largeIcon = null)
    {
        IGameNotification notification = manager.CreateNotification();

        if (notification == null)
        {
            return;
        }

        notification.Title = title;
        notification.Body = body;
        notification.Group = !string.IsNullOrEmpty(channelId) ? channelId : ChannelId;
        notification.DeliveryTime = deliveryTime;
        notification.SmallIcon = smallIcon;
        notification.LargeIcon = largeIcon;
        if (badgeNumber != null)
        {
            notification.BadgeNumber = badgeNumber;
        }

        PendingNotification notificationToDisplay = manager.ScheduleNotification(notification);
        notificationToDisplay.Reschedule = reschedule;
    }
}

