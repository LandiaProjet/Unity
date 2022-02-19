using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : Enemy
{
    public bool Enable;

    private void Start()
    {
        Enable = false;
    }

    public override void onDie()
    {
        Debug.Log("Enable");
        Enable = true;
        GetComponent<Animator>().Play("Lever");
    }
}
