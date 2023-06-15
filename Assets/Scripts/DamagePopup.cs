using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamagePopup : MonoBehaviour
{
    public GameObject canvas0;
    GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Text tmp_text = GetComponent<Text>();
        tmp_text.text = Player.GetComponent<PlayerController>().curStatus.att.ToString();
        tmp_text.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        tmp_text.DOFade(0f, 1.0f);
        tmp_text.DOColor(Color.red, 0.5f);
        transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 1);
        transform.DOMove(transform.position + Vector3.up * 3, 1).OnComplete(() =>
        {
            Destroy(canvas0);
        });
    }

}
