using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour {
    public Animator animator;
    private int levelToLoad;
    
    public void FadeToLevel(int sceneIndex) {
        levelToLoad = sceneIndex;
        StartCoroutine(FadeDelayedCoroutine(0.2f));
    }

    private IEnumerator FadeDelayedCoroutine(float delay) {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete() {
        SceneManager.LoadScene(levelToLoad);
    }
}
