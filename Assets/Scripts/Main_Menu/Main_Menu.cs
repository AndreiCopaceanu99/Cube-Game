using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    [SerializeField] GameObject[] Menu_Objects;

    private void Start()
    {
        Menu_Objects[0].SetActive(true);
        for(int i = 1; i<= Menu_Objects.Length- 1; i++)
        {
            Menu_Objects[i].SetActive(false);
        }
    }

    public void Endless()
    {
        SceneManager.LoadScene("Endless");
    }

    public void Back()
    {
        Menu_Objects[0].SetActive(true);
        for (int i = 1; i <= Menu_Objects.Length - 1; i++)
        {
            Menu_Objects[i].SetActive(false);
        }
    }

    public void Settings()
    {
        Menu_Objects[0].SetActive(false);
        Menu_Objects[1].SetActive(true);
    }

    public void Shop()
    {
        Menu_Objects[0].SetActive(false);
        Menu_Objects[2].SetActive(true);
    }

    public void Credits()
    {
        Menu_Objects[0].SetActive(false);
        Menu_Objects[3].SetActive(true);
    }
}
