using UnityEngine;


public class PauseMenu : MonoBehaviour
{

    public GameObject container;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            container.SetActive(true);
            Time.timeScale = 0;

        }
    }


    public void ResumeGame()
    {
        container.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
