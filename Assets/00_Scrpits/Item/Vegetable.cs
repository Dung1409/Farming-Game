using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    
    private Animator _anim;
    public State state;
    private SpriteRenderer _icon;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _icon = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        setState(State.Normal);
    }

    public void Development()
    {
        setState(State.Develop);
    }

    public void setState(State _state)
    {
        state = _state;
        _anim.SetInteger(Contant.State, (int)state);
    }

    public enum State
    {
        Normal,
        Develop
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == State.Normal)
        {
            return;
        }
        
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Vector3 pos = this.transform.position - new Vector3(0.5f, 0.5f, 0);
        Vector3Int tilePos = GameManager.intant.Soil.WorldToCell(pos);
        GameManager.intant.tilePos[tilePos] = false;
        string n = name.ToLower();
        Product pro = new Product(name: n , Icon : _icon);
        GameManager.intant.AddItem(n , pro);
    }
}   
