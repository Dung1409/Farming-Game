using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private Rigidbody2D _rigi;
    private Animator _anim;
    private State state;
    private SpriteRenderer _render;
    private void Awake()
    {
        state = State.Idle;
        _rigi = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _render = GetComponent<SpriteRenderer>();   
    }

    private void FixedUpdate()
    {
        Move();   
             
    }


    private void Move()
    {
        float axisX = Input.GetAxisRaw(Contant.Horizontal);
        float axisY = Input.GetAxisRaw(Contant.Vertical);
        Vector2 dir = new Vector2(axisX, axisY).normalized;
        state = State.Idle;
        if (dir != Vector2.zero) 
        {
            state = State.Travel;
        }
        if(dir.x > 0) 
        {
            _render.flipX = false;
        }
        else if(dir.x < 0)
        {
            _render.flipX = true;
        }
        _rigi.velocity = dir * speed;   
        _anim.SetInteger(Contant.State, (int)state);    
    }

    enum State
    {
        Idle,
        Travel
    }
}
