using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour
{
    [SerializeField]
    private Color color;

    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 충돌이 일어나는 순간 1회 호출
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        spriteRenderer.color = color;
    }

    /// <summary>
    /// 충돌이 유지되는 동안 매 프레임 호출
    /// </summary>
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(gameObject.name + 
            " : OnCollisionStay2D() 메소드 실행");
    }

    /// <summary>
    /// 충돌이 종료되는 순간 1회 호출
    /// </summary>
    private void OnCollisionExit2D(Collision2D collision)
    {
        spriteRenderer.color = Color.white;
    }
}
