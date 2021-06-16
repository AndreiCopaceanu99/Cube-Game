using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayFab_Controller : MonoBehaviour
{
    public static PlayFab_Controller PFC;

    string User_Email;
    string User_Password;
    string Username;

    [SerializeField] GameObject Login_Panel;

    private void OnEnable()
    {
        if(PlayFab_Controller.PFC == null)
        {
            PlayFab_Controller.PFC = this;
        }
        else
        {
            if(PlayFab_Controller.PFC != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Note: Setting title Id here can be skipped of you have set the value in Editor Extension already
        if(string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "2550"; //Please change this value to your own titleOd from Playfab Game Manager
        }

        //PlayerPrefs.DeleteAll();

        //var Request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        //PlayFabClientAPI.LoginWithCustomID(Request, On_Login_Success, On_Login_Failure);
        if (PlayerPrefs.HasKey("EMAIL"))
        {
            User_Email = PlayerPrefs.GetString("EMAIL");
            User_Password = PlayerPrefs.GetString("PASSWORD");
            var Request = new LoginWithEmailAddressRequest { Email = User_Email, Password = User_Password };
            PlayFabClientAPI.LoginWithEmailAddress(Request, On_Login_Success, On_Login_Failure);
        }
    }

    #region Login
    private void On_Login_Success(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", User_Email);
        PlayerPrefs.SetString("PASSWORD", User_Password);
        Login_Panel.SetActive(false);
        GetStats();
    }

    private void On_Register_Success(RegisterPlayFabUserResult Result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", User_Email);
        PlayerPrefs.SetString("PASSWORD", User_Password);
        Login_Panel.SetActive(false);
        GetStats();
    }

    private void On_Login_Failure(PlayFabError error)
    {
        var Register_Request = new RegisterPlayFabUserRequest { Email = User_Email, Password = User_Password, Username = Username };
        PlayFabClientAPI.RegisterPlayFabUser(Register_Request, On_Register_Success, On_Register_Failure);
    }

    private void On_Register_Failure(PlayFabError Error)
    {
        Debug.LogError(Error.GenerateErrorReport());
    }

    public void Get_User_Email(string Email_In)
    {
        User_Email = Email_In;
    }

    public void Get_User_Password(string Password_In)
    {
        User_Password = Password_In;
    }

    public void Get_Username(string Username_In)
    {
        Username = Username_In;
    }

    public void On_Click_Login()
    {
        var Request = new LoginWithEmailAddressRequest { Email = User_Email, Password = User_Password };
        PlayFabClientAPI.LoginWithEmailAddress(Request, On_Login_Success, On_Login_Failure);
    }
    #endregion Login

    public int Player_High_Score;
    public int Player_Coins;

    #region Player_Stats

    public void Set_Stats()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate { StatisticName = "Player_HighScore", Value = Player_High_Score },
                new StatisticUpdate { StatisticName = "Player_Coins", Value = Player_Coins },
                }
        },
        Result => { Debug.Log("User statistics updated"); },
        Error => { Debug.LogError(Error.GenerateErrorReport()); });
    }

    void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            On_Get_Stats,
            Error => Debug.LogError(Error.GenerateErrorReport())
        );
    }

    void On_Get_Stats(GetPlayerStatisticsResult Result)
    {
        Debug.Log("Received the following Statistics:");
        foreach(var Each_Stat in Result.Statistics)
        {
            Debug.Log("Statistic(" + Each_Stat.StatisticName + "): " + Each_Stat.Value);
            switch(Each_Stat.StatisticName)
            {
                case "Player_High_Score":
                    Player_High_Score = Each_Stat.Value;
                    break;
                case "Player_Coins":
                    Player_Coins = Each_Stat.Value;
                    break;
            }
        }
    }

    #endregion Player_Stats
}
