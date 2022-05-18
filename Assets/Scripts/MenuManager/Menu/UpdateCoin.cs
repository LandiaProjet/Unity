using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCoin : MonoBehaviour
{
    public GameObject text;

    void Update()
    {
        text.GetComponent<TMPro.TextMeshProUGUI>().SetText(PlayerData.getData().money.ToString());
    }
}
