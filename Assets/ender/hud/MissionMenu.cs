using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionMenu : MonoBehaviour
{
    public void toggleMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
