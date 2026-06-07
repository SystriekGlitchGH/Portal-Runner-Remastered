using UnityEngine;
using UnityEngine.UI;
public class ScenePortal : MonoBehaviour
{
    public SceneTransition st;
    public string sceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement pm))
        {
            pm.isTransitioning = true;
            st.FadeOut(sceneName);
        }
    }
}
