using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour {

    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    //private Text healthText;
    public Transform zombie;

    private void Update()
    {
        if (zombie == null)
            Destroy(gameObject);
        else
        {
            transform.position = zombie.position + transform.up * 1.3f;
        }
    }

    private void Start()
    {
        if(healthBarRect == null)
        {
            Debug.Log("Status indicator: No health bar object referenced");
        }
    }

    public void SetHealth(int cur, int max)
    {
        float value = (float) cur / max;
        healthBarRect.localScale = new Vector3(value, healthBarRect.localScale.y, healthBarRect.localScale.z);

        // TODO: Figure out how to display text on health bar without 
        // flipping when enemy turns left and right.
        //healthText.text = cur + "/" + max;
    }
}
