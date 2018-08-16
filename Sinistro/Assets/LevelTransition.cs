using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour {

    public Animator animator;
	
	// Update is called once per frame
	void Update () 
    {
	    	
	}

    public void FadeToLevel1(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
    }
}
