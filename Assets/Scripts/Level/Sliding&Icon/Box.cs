using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool click;
    public bool lihat;
    public SpriteRenderer img;
    public Sprite kotakbenar, kotaksalah, kotakdefault;

    public float ms;
    public Transform defaultPosition;
    // Start is called before the first frame update
    void Start()
    {
        click = false;
        img.sprite = kotakdefault;
    }

    // Update is called once per frame
    void Update()
    {
        if (click==true&&img.sprite==kotakdefault)
        {
            transform.position = Vector2.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), ms * Time.deltaTime);
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.transform.tag=="batas")
    //    {
    //        click = false;
    //        transform.position = defaultPosition.position;
    //    }
    //}
}
