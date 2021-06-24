using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Is responsable with the shop
public class Shop : MonoBehaviour
{
    public static Shop SH;

    [SerializeField] Image[] Images;
    [SerializeField] Text[] Prices;
    [SerializeField] Text[] Names;
    [SerializeField] Text[] Ownership;

    [SerializeField] Shop_Panel[] Panels;

    int Active_Panel;
    int Objects_Order;

    private void OnEnable()
    {
        SH = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Update_Skins();
        Active_Panel = 0;
        Objects_Order = 0;
        Update_Panels();
    }

    // Updates the panel with the proper images, names, price tags and ownership
    public void Update_Panels()
    {
        for (int i = 0; i <= Images.Length - 1; i++)
        {
            Images[i].sprite = Panels[Active_Panel].Object[Objects_Order + i].Object;
            Prices[i].text = Panels[Active_Panel].Object[Objects_Order + i].Price.ToString();
            Names[i].text = Panels[Active_Panel].Object[Objects_Order + i].Name;
            switch (Panels[Active_Panel].Object[Objects_Order + i].Ownership)
            {
                case 0:
                    {
                        Ownership[i].text = "Not owned";
                        Ownership[i].color = Color.red;
                        break;
                    }
                case 1:
                    {
                        Ownership[i].text = "Owned";
                        Ownership[i].color = Color.black;
                        break;
                    }
                case 2:
                    {
                        Ownership[i].text = "Equiped";
                        Ownership[i].color = Color.green;
                        break;
                    }
            }
        }
    }

    public void Unlock_Skin(int Index)
    {
        if(Ownership[Index].text == "Not owned")
        {
            Panels[Active_Panel].Object[Objects_Order + Index].Ownership = 1;
            Panels[Active_Panel].Object[Objects_Order + Index].Price = 0;
        }
        if(Ownership[Index].text == "Owned")
        {
            for(int i = 0; i < Panels[Active_Panel].Object.Length; i++)
            {
                if (Panels[Active_Panel].Object[i].Ownership == 2)
                {
                    Panels[Active_Panel].Object[i].Ownership = 1;
                }
            }

            Panels[Active_Panel].Object[Objects_Order + Index].Ownership = 2;
        }

        switch(Active_Panel)
        {
            case 0:
                Persistent_Data.PD.Balls[Objects_Order + Index] = true;
                PlayFab_Controller.PFC.Ball_Data = Persistent_Data.PD.Skins_Data_To_String(Active_Panel);
                if (Panels[Active_Panel].Object[Objects_Order + Index].Ownership == 2)
                {
                    Persistent_Data.PD.My_Ball = Objects_Order + Index;
                }
                break;
            case 1:
                Persistent_Data.PD.Platforms[Objects_Order + Index] = true;
                PlayFab_Controller.PFC.Platform_Data = Persistent_Data.PD.Skins_Data_To_String(Active_Panel);
                if (Panels[Active_Panel].Object[Objects_Order + Index].Ownership == 2)
                {
                    Persistent_Data.PD.My_Platform = Objects_Order + Index;
                }
                break;
            case 2:
                Persistent_Data.PD.Backgrounds[Objects_Order + Index] = true;
                PlayFab_Controller.PFC.Background_Data = Persistent_Data.PD.Skins_Data_To_String(Active_Panel);
                if (Panels[Active_Panel].Object[Objects_Order + Index].Ownership == 2)
                {
                    Persistent_Data.PD.My_Background = Objects_Order + Index;
                }
                break;
        }
        PlayFab_Controller.PFC.Skin_Selection = Persistent_Data.PD.My_Ball.ToString() + Persistent_Data.PD.My_Platform.ToString() + Persistent_Data.PD.My_Background.ToString(); ;
        PlayFab_Controller.PFC.Set_User_Data();
        Update_Panels();
    }

    public void Update_Skins()
    {
        for(int i = 0; i < Persistent_Data.PD.Balls.Length; i++)
        {
            if(Persistent_Data.PD.Balls[i] == true)
            {
                Panels[0].Object[i].Ownership = 1;
                Panels[0].Object[i].Price = 0;
            }
            else
            {
                Panels[0].Object[i].Ownership = 0;
            }
            if(i == Persistent_Data.PD.My_Ball)
            {
                Panels[0].Object[i].Ownership = 2;
            }
            PlayFab_Controller.PFC.Ball_Data = Persistent_Data.PD.Skins_Data_To_String(0);
        }
        for (int i = 0; i < Persistent_Data.PD.Platforms.Length; i++)
        {
            if (Persistent_Data.PD.Platforms[i] == true)
            {
                Panels[1].Object[i].Ownership = 1;
                Panels[1].Object[i].Price = 0;
            }
            else
            {
                Panels[1].Object[i].Ownership = 0;
            }
            if (i == Persistent_Data.PD.My_Platform)
            {
                Panels[1].Object[i].Ownership = 2;
            }
            PlayFab_Controller.PFC.Platform_Data = Persistent_Data.PD.Skins_Data_To_String(1);
        }
        for (int i = 0; i < Persistent_Data.PD.Backgrounds.Length; i++)
        {
            if (Persistent_Data.PD.Backgrounds[i] == true)
            {
                Panels[2].Object[i].Ownership = 1;
                Panels[2].Object[i].Price = 0;
            }
            else
            {
                Panels[2].Object[i].Ownership = 0;
            }
            if (i == Persistent_Data.PD.My_Background)
            {
                Panels[2].Object[i].Ownership = 2;
            }
            PlayFab_Controller.PFC.Background_Data = Persistent_Data.PD.Skins_Data_To_String(2);
        }
        PlayFab_Controller.PFC.Set_User_Data();
    }

    //Changes the objects to balls
    public void Ball_Button()
    {
        if(Active_Panel != 0)
        {
            Objects_Order = 0;
            Active_Panel = 0;
            Update_Panels();
        }
    }

    //Changes the objects to platforms
    public void Platform_Button()
    {
        if (Active_Panel != 1)
        {
            Objects_Order = 0;
            Active_Panel = 1;
            Update_Panels();
        }
    }

    //Changes the objects to backgrounds
    public void Background_Button()
    {
        if (Active_Panel != 2)
        {
            Objects_Order = 0;
            Active_Panel = 2;
            Update_Panels();
        }
    }

    // Moves the images to the left
    public void Left_Button()
    {
        if (Objects_Order > 0)
        {
            Objects_Order--;
            Update_Panels();
        }
    }

    // Moves the images to the right
    public void Right_Button()
    {
        if (Objects_Order < Panels[Active_Panel].Object.Length - 3)
        {
            Objects_Order++;
            Update_Panels();
        }
    }

    // Struct for every type of object
    [System.Serializable]
    struct Shop_Panel
    {
        public string Name;
        public Objects[] Object;
    }

    // Struct for every object
    [System.Serializable]
    struct Objects
    {
        public string Name;
        public Sprite Object;
        public int Price;
        public int Ownership;
    }
}