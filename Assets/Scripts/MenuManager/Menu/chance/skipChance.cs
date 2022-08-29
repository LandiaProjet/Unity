using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipChance : MonoBehaviour
{
    private void OnEnable()
    {
        if (isPlaying.instance.getChance())
        {
            StartCoroutine(waitBeforeEnd());
        } else
        {
            MenuManager.instance.CloseMenu("chance");
            MenuManager.instance.OpenMenu("PopupRetry", 11);
            MenuManager.instance.OpenMenu("PopupDefeat", 10);
        }
    }

    IEnumerator waitBeforeEnd()
    {
        yield return new WaitForSeconds(2f);
        isPlaying.instance.deleteItem(3);
        MenuManager.instance.CloseMenu("chance");
        endingLevel.instance.Retrylevel();
    }
}
