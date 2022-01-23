using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Get_Login_Components : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Button Register_Button;
    [SerializeField] Button Login_Button;

    void Start()
    {
        Button Register = Register_Button.GetComponent<Button>();
        Register.onClick.AddListener(PlayFab_Controller.PFC.On_Click_Register);

        Button Login = Login_Button.GetComponent<Button>();
        Login.onClick.AddListener(PlayFab_Controller.PFC.On_Click_Login);
    }

    public void Get_User_Email(string Email_In)
    {
        PlayFab_Controller.PFC.Get_User_Email(Email_In);
    }

    public void Get_User_Password(string Password_In)
    {
        PlayFab_Controller.PFC.Get_User_Password(Password_In);
    }

    public void Get_Username(string Username_In)
    {
        PlayFab_Controller.PFC.Get_Username(Username_In);
    }
}
