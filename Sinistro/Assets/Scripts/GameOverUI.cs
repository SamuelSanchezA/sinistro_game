using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

    public void Quit()
    {
        Debug.Log("Application Quit!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Retry()
    {
        Debug.Log("Application Retry!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
