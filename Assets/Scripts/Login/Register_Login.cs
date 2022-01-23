using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register_Login : MonoBehaviour
{
    [SerializeField] GameObject Login;
    [SerializeField] GameObject Register;

    // Start is called before the first frame update
    void Start()
    {
        Login.SetActive(false);
    }

    public void Login_Button()
    {
        Login.SetActive(true);
        Register.SetActive(false);
    }

    public void Register_Button()
    {
        Login.SetActive(false);
        Register.SetActive(true);
    }
}
