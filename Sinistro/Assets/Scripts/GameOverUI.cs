using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

    public void Quit()
    {
        Debug.Log("Application Quit!");
        Application.Quit();
    }

    public void Retry()
    {
        Debug.Log("Application Retry!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
