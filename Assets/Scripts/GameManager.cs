using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonManager<GameManager>
{
    public bool isScroll = true;

    // µ·
    public float Money = 1000;
    public Text MoneyTxt;

    private void Awake()
    {
        MoneyTxt.text = Money.ToString();
    }

    public void SetMoney(float money)
    {
        Money += money;
        StartCoroutine(Count(Money, Money - money));
    }

    IEnumerator Count(float target, float current)
    {
        float duration = 0.5f;
        float offset = (target - current) / duration;

        while(current < target)
        {
            current += offset * Time.deltaTime;
            MoneyTxt.text = string.Format("{0:n0}", (int)current);

            yield return null;
        }

        current = target;
        MoneyTxt.text = string.Format("{0:n0}", (int)current);
    }
}
