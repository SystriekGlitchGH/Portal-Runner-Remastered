using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    public float transitionTime;
    public void Start()
    {
        FadeIn();
    }
    public void FadeIn()
    {
        StartCoroutine(FadeInTimer());
    }
    public void FadeOut(string sceneName)
    {
        StartCoroutine(FadeOutTimer(sceneName));
    }
    private IEnumerator FadeOutTimer(string sceneName)
    {
        float elapsedTime = 0f;
        while(elapsedTime < transitionTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Clamp01(elapsedTime / transitionTime);
            yield return null;
        }
        cg.alpha = transitionTime;
        SceneManager.LoadScene(sceneName);
    }
    private IEnumerator FadeInTimer()
    {
        float elapsedTime = 0f;
        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Clamp01(1 -(elapsedTime / transitionTime));
            yield return null;
        }
        cg.alpha = 0;
    }
}
