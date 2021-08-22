using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void On_Start()
    {
        // SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
        SceneManager.LoadScene(1);
    }

    public void Application_Quit()
    {
        Application.Quit();
    }
}
