using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Is responsable with managing the game
public class Manager : MonoBehaviour
{
    Camera camera;

    [SerializeField] GameObject[] Cubes;
    [SerializeField] Ball_Movement Ball;

    float Cube_Y;
    float Camera_Y;
    float Ball_Y;

    [SerializeField] GameObject Lose_Panel;

    [SerializeField] GameObject Ad_Panel;
    [SerializeField] Text Timer;
    float Ad_Timer;
    [SerializeField] Button Ad_Buton;
    bool Second_Chance;

    [SerializeField] Text Points;

    public int Difficulty;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Lose_Panel.SetActive(false);
        Ad_Panel.SetActive(false);

        Ad_Timer = 3;
        Second_Chance = false;

        camera = Camera.main;

        Difficulty = 0;

        // Positions all the platforms at equal distances by taking as reference the one abowe each other
        for (int i = 1; i <= Cubes.Length - 1; i++)
        {
            Cubes[i].transform.position = new Vector3(0, Cubes[i - 1].transform.position.y - 2f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Points_UI();

        Update_Cubes();

        Update_Camera();
        if(Ad_Panel.active)
        {
            Ad();
        }

        if ((int)-Ball_Y / 20 != Difficulty && (int)-Ball_Y / 20 != 0)
        {
            Increase_Difficulty();
        }
    }

    // Checks if the platforms are in front of the camera, if not, they are moved at the end of the array and beneath the lowest platform in the scene. This way the performances are improved
    void Update_Cubes()
    {
        Cube_Y = Cubes[0].transform.position.y;
        Camera_Y = camera.transform.position.y;

        if (Cube_Y - Camera_Y >= 6f)
        {
            GameObject Temp_Cube = Cubes[0];
            for (int i = 0; i < Cubes.Length - 1; i++)
            {
                Cubes[i] = Cubes[i + 1];
            }
            Cubes[Cubes.Length - 1] = Temp_Cube;
            Cubes[Cubes.Length - 1].transform.position = new Vector3(Cubes[Cubes.Length - 1].transform.position.x, Cubes[Cubes.Length - 2].transform.position.y - 2f, 0);
        }
    }

    // The camera is updated bassed on the ball's position
    void Update_Camera()
    {
        Ball_Y = Ball.transform.position.y;
        Camera_Y = camera.transform.position.y;

        // If the ball goes off screen, the game is over
        if(Camera_Y - Ball_Y >= camera.orthographicSize)
        {
            Lose();
            PlayFab_Controller.PFC.Leaderboard_Displayed = true;
        }
        else
        {
            PlayFab_Controller.PFC.Leaderboard_Displayed = false;
        }
    }

    void Increase_Difficulty()
    {
        Difficulty = (int)-Ball_Y / 20;
    }

    // The game is stopped and the game over screen pups up
    void Lose()
    {
        bool High_Score_Ready = false;
        if (-(int)Ball_Y > PlayFab_Controller.PFC.Player_High_Score)
        {
            PlayFab_Controller.PFC.Player_High_Score = (int)-Ball_Y;
            PlayFab_Controller.PFC.Start_Cloud_Update_Palyer_Stats();
            High_Score_Ready = true;
        }
        else
        {
            High_Score_Ready = true;
        }
        if (High_Score_Ready)
        {
            Lose_Panel.SetActive(true);
            if (!PlayFab_Controller.PFC.Leaderboard_Displayed)
            {

                PlayFab_Controller.PFC.Get_Leaderboard();
            }
            Lose_Panel.SetActive(true);
            Time.timeScale = 0;
            // Checks if the player is on the second chance
            if (Second_Chance)
            {
                // Deactivates the ad button
                Ad_Buton.interactable = false;
            }
        }
    }

    // Resets the ball, by giving one more chance to the player and plays an ad
    void Ad()
    {
        Ball.transform.position = new Vector2(0, camera.transform.position.y + 2);
        Time.timeScale = 1;
        Ad_Timer -= Time.deltaTime;
        Timer.text = Mathf.FloorToInt(Ad_Timer).ToString();
        //Debug.Log(Ad_Timer);
        if(Ad_Timer <= 0)
        {
            Ad_Panel.SetActive(false);
            Lose_Panel.SetActive(false);
            PlayFab_Controller.PFC.Close_Leaderboard_Panel();
            //Ball.transform.position = new Vector2(0, camera.transform.position.y + 2);
        }
    }

    // Displayes the points that the player got
    void Points_UI()
    {
        Points.text = "Points: " + (int) -Ball_Y;
    }

    // Loads the main menu scene
    public void Main_Menu_Button()
    {
        PlayFab_Controller.PFC.Player_Coins += (int)-Ball_Y;
        PlayFab_Controller.PFC.Start_Cloud_Update_Palyer_Stats();
        PlayFab_Controller.PFC.Close_Leaderboard_Panel();
        SceneManager.LoadScene("Main_Menu");
    }

    // Activates the ad
    public void Continue()
    {
        Second_Chance = true;
        Ad_Panel.SetActive(true);
    }
}
