using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int HP = 100;
    public float monSpeed = 5;
    public Transform StartPosition;

    private Animator animator;

    private float playerHP = 0;
    private int cntLoop = 0;
    private PlayerController _player;

    public int att = 5;

    private void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }


    private void Update()
    {
        if(GameManager.Instance.isScroll)
        {
            transform.Translate(Vector2.left * Time.deltaTime * monSpeed);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            float normalizeTime = 
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float normalizeTimeInCurrentLoop =
                normalizeTime - Mathf.Floor(normalizeTime);


            if(normalizeTimeInCurrentLoop >= 0.9f &&
                normalizeTime > cntLoop)
            {
                playerHP = _player.Damage(att);
                cntLoop += 1;

                if(playerHP <= 0)
                {
                    animator.SetBool("attack", false);
                    cntLoop = 0;
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            animator.SetBool("attack", true);
            _player = collision.gameObject.GetComponent<PlayerController>();
        }
    }
}
