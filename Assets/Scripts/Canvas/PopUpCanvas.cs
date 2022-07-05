using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpCanvas : MonoBehaviour
{
    //audio manager
    [SerializeField] private SoundManager audiomanager;
    [SerializeField] private int nomorPopup;//menentukan sound popup mana yang akan keluar

    public GameObject Panel;

    public GameObject popupButton;

    private void Awake()
    {
       audiomanager=FindObjectOfType<SoundManager>();
    }
    public void OpenPanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(true);
        }

        //GameObject popupButton = GameObject.FindWithTag("PopupButton");
        //popupButton.SetActive(false);
    }
    
    public void ClosePanel()
    {
        //GameObject puzzle = GameObject.FindWithTag("PuzzleManager");
        //puzzle.SetActive(false);

        //GameObject puzzle = GameObject.FindWithTag("MainCamera");
        //puzzle.SetActive(false);
        audiomanager.popupMetohod(nomorPopup);

        FindObjectOfType<PuzzleManager>().keluar();
    }
}
