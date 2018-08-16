using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    AudioManager audioManager;

    public Animator animator;
    private int levelToLoad;

    private void Start()
    {
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No audiomanager found");
        }
        audioManager.PlaySound("Homenaje");
    }

    public void StartGame()
    {
        FadeToLevel1(1);

        audioManager.PlaySound(pressButtonSound);

    }

    public void QuitGame()
    {
        audioManager.PlaySound(pressButtonSound);
        Debug.Log("We quit the game");
        Application.Quit();
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }

    public void FadeToLevel1(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
        audioManager.StopSound("Homenaje");
        audioManager.PlaySound("DrumTransition");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        audioManager.PlaySound("BlueLevelMusic");   
    }

}
