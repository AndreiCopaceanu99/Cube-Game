using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistent_Data : MonoBehaviour
{
    public static Persistent_Data PD;

    public bool[] Balls;
    public bool[] Platforms;
    public bool[] Backgrounds;

    public int My_Ball;
    public int My_Platform;
    public int My_Background;

    private void OnEnable()
    {
        Persistent_Data.PD = this;
    }

    public void Skins_String_To_Data(string Skins_In, string Skin_Type)
    {
        Debug.Log(Skins_In);
        switch(Skin_Type)
        {
            case "Balls":
                for (int i = 0; i < Skins_In.Length; i++)
                {
                    if (int.Parse(Skins_In[i].ToString()) > 0)
                    {
                        Balls[i] = true;
                    }
                    else
                    {
                        Balls[i] = false;
                    }
                }
                break;
            case "Platforms":
                for (int i = 0; i < Skins_In.Length; i++)
                {
                    if (int.Parse(Skins_In[i].ToString()) > 0)
                    {
                        Platforms[i] = true;
                    }
                    else
                    {
                        Platforms[i] = false;
                    }
                }
                break;
            case "Backgrounds":
                for (int i = 0; i < Skins_In.Length; i++)
                {
                    if (int.Parse(Skins_In[i].ToString()) > 0)
                    {
                        Backgrounds[i] = true;
                    }
                    else
                    {
                        Backgrounds[i] = false;
                    }
                }
                break;
            case "Skin_Selection":
                My_Ball = int.Parse(Skins_In[0].ToString());
                My_Platform = int.Parse(Skins_In[1].ToString());
                My_Background = int.Parse(Skins_In[2].ToString());
                break;
        }
        Shop.SH.Update_Skins();
    }
    
    public string Skins_Data_To_String(int Panel)
    {
        string To_String = "";
        switch(Panel)
        {
            case 0:
                for (int i = 0; i < Balls.Length; i++)
                {
                    if (Balls[i] == true)
                    {
                        To_String += "1";
                    }
                    else
                    {
                        To_String += "0";
                    }
                }
                break;
            case 1:
                for (int i = 0; i < Platforms.Length; i++)
                {
                    if (Platforms[i] == true)
                    {
                        To_String += "1";
                    }
                    else
                    {
                        To_String += "0";
                    }
                }
                break;
            case 2:
                for (int i = 0; i < Backgrounds.Length; i++)
                {
                    if (Backgrounds[i] == true)
                    {
                        To_String += "1";
                    }
                    else
                    {
                        To_String += "0";
                    }
                }
                break;
            case 3:
                To_String = My_Ball.ToString() + My_Platform.ToString() + My_Background.ToString();
                break;
        }
        return To_String;
    }
}
