using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Is responsable with the shop
public class Shop : MonoBehaviour
{
    [SerializeField] Image[] Images;
    [SerializeField] Text[] Prices;
    [SerializeField] Text[] Names;
    [SerializeField] Text[] Ownership;

    [SerializeField] Shop_Panel[] Panels;

    int Active_Panel;
    int Objects_Order;

    // Start is called before the first frame update
    void Start()
    {
        Active_Panel = 0;
        Objects_Order = 0;
        Update_Panels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Updates the panel with the proper images, names, price tags and ownership
    void Update_Panels()
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