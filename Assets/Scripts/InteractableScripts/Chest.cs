using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    MenuController menuController;
    [SerializeField] GameObject cardSelectPanel;
    [SerializeField] UnityEvent OnPlayerEnterChest;
    [SerializeField] UnityEvent OnPlayerLeaveChest;

    private void Awake()
    {
        menuController = FindObjectOfType<MenuController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Chest Triggred");
        OnPlayerEnterChest.Invoke();
        menuController.PauseGame();
        menuController.DisablePlayer();
        Debug.Log("Check Panel");
        menuController.SetCardsValues();
        cardSelectPanel.gameObject.SetActive(true);


        //Destroy object
        GameObject.Destroy(this);
    }

    private void OnTriggerExit(Collider other)
    {
        OnPlayerLeaveChest.Invoke();
    }

}
