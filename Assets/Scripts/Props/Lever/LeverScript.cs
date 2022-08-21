using UnityEngine;

public class LeverScript : Enemy
{
    public bool isEnable;

    public override void onDie()
    {
        isEnable = !isEnable;
        GetComponent<Animator>().SetBool("isEnable", isEnable);
    }
}
