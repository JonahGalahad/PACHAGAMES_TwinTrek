using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_UIMenuAudio : MonoBehaviour
{
    [SerializeField] private GameObject panelJugar;
    [SerializeField] private GameObject panelOption;
    [SerializeField] private GameObject panelControls;
    [SerializeField] private GameObject panelMonsBook;
    void Update()
    {
        ClickButtonJugar();
        ClickButtonOption();
        ClickButtonControls();
        ClickButtonMonsBook();
    }
    private void ClickButtonJugar() {
        if(panelJugar.activeInHierarchy) Manager_UIAudio.instance.PlayClickEvent();
    }
    private void ClickButtonOption() {
        if(panelOption.activeInHierarchy) Manager_UIAudio.instance.PlayClickEvent();
    }
    private void ClickButtonControls() {
        if(panelControls.activeInHierarchy) Manager_UIAudio.instance.PlayClickEvent();
    }
    private void ClickButtonMonsBook() {
        if(panelMonsBook.activeInHierarchy) Manager_UIAudio.instance.PlayClickEvent();
    }
}
