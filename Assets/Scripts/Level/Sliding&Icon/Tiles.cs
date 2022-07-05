using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public Vector3 posisitarget;
    private Vector3 posisibenar;
    private SpriteRenderer _sprite;

    public bool benar;
    void Awake()
    {
        posisitarget = transform.position;
        posisibenar = transform.position;
        _sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, posisitarget, 0.5f);
        if (posisitarget==posisibenar)
        {
            _sprite.color = Color.white;
            benar = true;
        }
        else
        {
            _sprite.color = Color.gray;
            benar = false;
        }
    }
}
