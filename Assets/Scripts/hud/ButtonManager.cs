using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    public GameObject player;
    public GameObject ButtonAttack;
    public GameObject Icon;
    public Sprite sword;
    public Sprite arrow;

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];

        Image image = Icon.GetComponentInChildren<Image>();
        EventTrigger trigger = ButtonAttack.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;

        PlayerSwordAttack playerSwordAttack;
        PlayerBowAttack playerBowAttack;

        if (playerBowAttack = player.GetComponent<PlayerBowAttack>())
        {
            entry.callback.AddListener((data) => { playerBowAttack.shoot(); });
            trigger.triggers.Add(entry);
            image.sprite = arrow;
        } else if (playerSwordAttack = player.GetComponent<PlayerSwordAttack>())
        {
            entry.callback.AddListener((data) => { playerSwordAttack.Attack(); });
            trigger.triggers.Add(entry);
            image.sprite = sword;
        }
        else
        {
            Destroy(ButtonAttack);
        }
    }
}
