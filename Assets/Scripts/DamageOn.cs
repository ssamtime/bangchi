using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOn : MonoBehaviour
{
    public GameObject prefabDamage;

    public void DamageTxt()
    {
        GameObject inst = Instantiate(prefabDamage, transform);
        inst.transform.SetParent(transform);
    }
}
