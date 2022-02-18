using System.Collections;
using UnityEngine;

public class UpgraderCount
{
    float TimeBeforeStart;
    float value;
    float time;
    TMPro.TextMeshProUGUI text;

    public UpgraderCount(float TimeBeforeStart, float value, float time, TMPro.TextMeshProUGUI text)
    {
        this.TimeBeforeStart = TimeBeforeStart;
        this.value = value;
        this.time = time;
        this.text = text;
    }

    public IEnumerator StartAnimation()
    {
        float count = 0;

        yield return new WaitForSeconds(TimeBeforeStart);
        text.text = count.ToString();
        while (count < value)
        {
            text.text = count.ToString();
            count++;
            yield return new WaitForSeconds(time);
        }
        text.text = value.ToString();
    }
}
