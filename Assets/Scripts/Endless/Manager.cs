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

    [SerializeField] Text Points;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Lose_Panel.SetActive(false);

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

        Lose();
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

    void Lose()
    {
        Ball_Y = Ball.transform.position.y;
        Camera_Y = camera.transform.position.y;

        if(Camera_Y - Ball_Y >= 6f)
        {
            Lose_Panel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void Points_UI()
    {
        Points.text = "Points: " + -Mathf.Round(Ball_Y * 100f) / 100f;
    }

    public void Retry_Button()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
