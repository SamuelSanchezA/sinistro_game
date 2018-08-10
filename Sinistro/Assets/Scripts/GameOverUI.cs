using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

    [SerializeField]
    string mouseOverSound = "ButtonHover";

    [SerializeField]
    string buttonPressSound = "ButtonPress";

    // cache
    private AudioManager audioManager;

    private void Start()
    {
        // caching
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("PANIC!!! No AudioManager found!!!");
        }
    }

    public void Quit()
    {
        audioManager.PlaySound(buttonPressSound);

        Debug.Log("Application Quit!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Retry()
    {
        audioManager.PlaySound(buttonPressSound);

        Debug.Log("Application Retry!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(mouseOverSound);
    }
}
