using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayFab_Controller : MonoBehaviour
{
    public static PlayFab_Controller PFC;

    string User_Email;
    string User_Password;
    public string Username;
    string My_ID;

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

        if (PlayerPrefs.HasKey("EMAIL"))
        {
            User_Email = PlayerPrefs.GetString("EMAIL");
            User_Password = PlayerPrefs.GetString("PASSWORD");
            var Request = new LoginWithEmailAddressRequest { 
                Email = User_Email, 
                Password = User_Password, 
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams { 
                    GetPlayerProfile = true } 
            };
            PlayFabClientAPI.LoginWithEmailAddress(Request, On_Login_Success, On_Login_Failure);
        }
    }

    private void Update()
    {

    }

    #region Login
    private void On_Login_Success(LoginResult Result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", User_Email);
        PlayerPrefs.SetString("PASSWORD", User_Password);
        GetStats();
        SceneManager.LoadScene("Main_Menu");

        My_ID = Result.PlayFabId;
        if (Result.InfoResultPayload.PlayerProfile != null)
        {
            Username = Result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        Debug.Log(Username);

        Get_Player_Data();
    }

    private void On_Register_Success(RegisterPlayFabUserResult Result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", User_Email);
        PlayerPrefs.SetString("PASSWORD", User_Password);

        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        { DisplayName = Username }, On_Display_Name, On_Login_Failure);
        GetStats();
        SceneManager.LoadScene("Main_Menu");

        My_ID = Result.PlayFabId;

        Set_User_Data();
    }

    void On_Display_Name(UpdateUserTitleDisplayNameResult Result)
    {
        Debug.Log(Result.DisplayName + " is your new display name");
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
        var Request = new LoginWithEmailAddressRequest { 
            Email = User_Email, 
            Password = User_Password,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(Request, On_Login_Success, On_Login_Failure);
    }
    #endregion Login

    public void Clear_Login_Data()
    {
        PlayerPrefs.DeleteAll();
    }

    public int Player_High_Score;
    public int Player_Coins;

    #region Player_Stats

    public void Set_Stats()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate { StatisticName = "Player_High_Score", Value = Player_High_Score },
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

    //Build the request object and access the API
    public void Start_Cloud_Update_Palyer_Stats()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "Update_Player_Stats", // Arbitrary function name (must exist in the upload cloud.js file)
            FunctionParameter = new { High_Score = Player_High_Score, Coins = Player_Coins }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PalyStream
        }, On_Cloud_Update_Stats, On_Error_Shared);
    }

    private static void On_Cloud_Update_Stats(ExecuteCloudScriptResult Result)
    {
        // Cloud Script returns arbitrary results, so you have to evaluate them one step and one parameter at a time
        Debug.Log(PlayFab.PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer));
        JsonObject Json_Result = (JsonObject)Result.FunctionResult;
        object Message_Value;
        Json_Result.TryGetValue("Message_Value", out Message_Value); // note how "Message_Value" directly corresponds to the JSON values set in CloudScript
        Debug.Log((string)Message_Value);
    }

    private static void On_Error_Shared(PlayFabError Error)
    {
        Debug.Log(Error.GenerateErrorReport());
    }


    #endregion Player_Stats

    public GameObject Leaderboard_Panel;
    public GameObject Listing_Prefab;
    public Transform Listing_Container;
    public bool Leaderboard_Displayed;

    #region Leaderboard

    public void Get_Leaderboard()
    {
        var Request_Leaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "Player_High_Score", MaxResultsCount = 20 };
        PlayFabClientAPI.GetLeaderboard(Request_Leaderboard, On_Get_Leaderboard, On_Error_Leaderboard);
    }

    void On_Get_Leaderboard(GetLeaderboardResult Result)
    {
        Leaderboard_Panel = GameObject.Find("/Canvas/Lose_Panel/Leaderboard");
        Listing_Container = GameObject.Find("Layout_Group").transform;

        foreach (PlayerLeaderboardEntry Player in Result.Leaderboard)
        {
            if (Listing_Container.childCount < Result.Leaderboard.Count)
            {
                GameObject Temp_Listing = Instantiate(Listing_Prefab, Listing_Container);
                Leaderboard_Listing LL = Temp_Listing.GetComponent<Leaderboard_Listing>();
                LL.Player_Name_Text.text = Player.DisplayName;
                if(Player.PlayFabId == My_ID && Player.StatValue < Player_High_Score)
                {
                    Player.StatValue = Player_High_Score;
                }
                LL.Player_Score_Text.text = Player.StatValue.ToString();

                Debug.Log(Player.DisplayName + ": " + Player.StatValue);
            }
        }
    }

    public void Close_Leaderboard_Panel()
    {
        Debug.Log("NO");
        for(int i = Listing_Container.childCount - 1; i >= 0 ; i--)
        {
            Destroy(Listing_Container.GetChild(i).gameObject);
        }
    }

    void On_Error_Leaderboard(PlayFabError Error)
    {
        Debug.LogError(Error.GenerateErrorReport());
    }

    #endregion Leaderboard

    #region Player_Data

    public string Ball_Data;
    public string Platform_Data;
    public string Background_Data;
    public string Skin_Selection;

    public void Get_Player_Data()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = My_ID,
            Keys = null
        }, User_Data_Success, On_Error_Leaderboard);
    }

    void User_Data_Success(GetUserDataResult Result)
    {
        if(Result.Data == null || !Result.Data.ContainsKey("Balls") || !Result.Data.ContainsKey("Platforms") || !Result.Data.ContainsKey("Backgrounds") || !Result.Data.ContainsKey("Skin_Selection"))
        {
            Debug.Log("Skins not set");
        }
        else
        {
            Ball_Data = Result.Data["Balls"].Value;
            Persistent_Data.PD.Skins_String_To_Data(Result.Data["Balls"].Value, "Balls");

            Platform_Data = Result.Data["Platforms"].Value;

            Persistent_Data.PD.Skins_String_To_Data(Result.Data["Platforms"].Value, "Platforms");
            Background_Data = Result.Data["Backgrounds"].Value;
            Persistent_Data.PD.Skins_String_To_Data(Result.Data["Backgrounds"].Value, "Backgrounds");

            if(Result.Data["Skin_Selection"].Value == null)
            {
                Result.Data["Skin_Selection"].Value = "000";
            }
            Skin_Selection = Result.Data["Skin_Selection"].Value;
            Persistent_Data.PD.Skins_String_To_Data(Result.Data["Skin_Selection"].Value, "Skin_Selection");
        }
    }

    public void Set_User_Data()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"Balls", Ball_Data },
                {"Platforms", Platform_Data },
                {"Backgrounds", Background_Data },
                {"Skin_Selection", Skin_Selection }
            }
        }, Set_Data_Success, On_Error_Leaderboard);
    }

    void Set_Data_Success(UpdateUserDataResult Result)
    {
        Debug.Log(Result.DataVersion);
        for (int i = 0; i <= 3; i++)
        {
            Persistent_Data.PD.Skins_Data_To_String(i);
        }
    }

    #endregion Player_Data
}
