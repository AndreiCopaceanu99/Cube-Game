using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Lose_Panel.SetActive(false);
        Ad_Panel.SetActive(false);

        Ad_Timer = 3;
        Second_Chance = false;

        camera = Camera.main;

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
    }

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

    void Update_Camera()
    {
        Ball_Y = Ball.transform.position.y;
        Camera_Y = camera.transform.position.y;

        if(Camera_Y - Ball_Y >= 6f)
        {
            Lose();
        }
    }

    void Lose()
    {
        Lose_Panel.SetActive(true);
        Time.timeScale = 0;
        if(Second_Chance)
        {
            Ad_Buton.interactable = false;
        }
    }

    void Ad()
    {
        Ball.transform.position = new Vector2(0, Ball_Y);
        Time.timeScale = 1;
        Ad_Timer -= Time.deltaTime;
        Timer.text = Mathf.FloorToInt(Ad_Timer).ToString();
        Debug.Log(Ad_Timer);
        if(Ad_Timer <= 0)
        {
            Ad_Panel.SetActive(false);
            Lose_Panel.SetActive(false);

            Ball.transform.position = new Vector2(0, camera.transform.position.y + 2);
            //Time.timeScale = 1;
        }
    }

    void Points_UI()
    {
        Points.text = "Points: " + -Mathf.Round(Ball_Y * 100f) / 100f;
    }

    public void Main_Menu_Button()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void Continue()
    {
        Second_Chance = true;
        Ad_Panel.SetActive(true);
    }
}
