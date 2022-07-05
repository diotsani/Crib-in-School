using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batas : MonoBehaviour
{
    public slidingpuzzlescript sldps;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Box")
        {
            sldps.waktu.waktu -= 5;
            sldps.audiomanager.popupMetohod(3);
        }
        //jika box benar terkena tag
        if (collision.transform.tag=="Box2")
        {
            sldps.jawabanBenar++;
            sldps.audiomanager.slidingPuzzleMetohod(1);
            sldps.solved = true;
        }

        foreach (var item in sldps.boxmanager)
        {
            item.click = false;
            item.transform.position = item.defaultPosition.position;
        }
    }
}
