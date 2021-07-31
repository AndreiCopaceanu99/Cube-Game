﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// It is responsable with the main menu manager
public class Main_Menu : MonoBehaviour
{
    [SerializeField] GameObject[] Menu_Objects;
    [SerializeField] Text Username;

    private void Start()
    {
        Menu_Objects[0].SetActive(true);
        for(int i = 1; i<= Menu_Objects.Length- 1; i++)
        {
            Menu_Objects[i].SetActive(false);
        }
        PlayFab_Controller.PFC.Leaderboard_Displayed = false;

        Username.text = PlayFab_Controller.PFC.Username;
    }

    private void Update()
    {
        if (Username.text == "")
        {
            Username.text = PlayFab_Controller.PFC.Username;
        }
    }

    // Load the endless scene
    public void Endless()
    {
        SceneManager.LoadScene("Endless");
    }

    // Closes all the panels and opens the one for the main menu
    public void Back()
    {
        Menu_Objects[0].SetActive(true);
        for (int i = 1; i <= Menu_Objects.Length - 1; i++)
        {
            Menu_Objects[i].SetActive(false);
        }
    }

    // Opens the settings panel
    public void Settings()
    {
        Menu_Objects[0].SetActive(false);
        Menu_Objects[1].SetActive(true);
    }

    // Opens the shop panel
    public void Open_Shop()
    {
        Shop.SH.Update_Skins();
        Menu_Objects[0].SetActive(false);
        Menu_Objects[2].SetActive(true);
        Shop.SH.Update_Panels();
    }

    // Opens the credits panel
    public void Credits()
    {
        Menu_Objects[0].SetActive(false);
        Menu_Objects[3].SetActive(true);
    }

    public void Sign_Out()
    {
        PlayFab_Controller.PFC.Clear_Login_Data();
        SceneManager.LoadScene("Login");
    }
}
