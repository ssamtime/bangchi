using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : SingletonManager<BuffManager>
{
    public GameObject buffPrefab;

    public void CreateBuff(string type,float per, float du, Sprite icon)
    {
        GameObject go = Instantiate(buffPrefab,transform);
        go.GetComponent<Buff>().Init(type, per, du);
        go.GetComponent<Image>().sprite = icon;
    }
}
