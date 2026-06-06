using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public PlayerMovement pm;
    public Animator anim;
    public SpriteRenderer sr;

    private void Update()
    {
        if(pm.getDirectionX() != 0)
            anim.SetBool("isMoving", true);
        else
            anim.SetBool("isMoving", false);

        if(pm.topSpeed == pm.topRunSpeed)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

        if (pm.hasUpgrade1)
            anim.SetBool("hasUpgrade1", true);
        if (pm.hasUpgrade2)
            anim.SetBool("hasUpgrade2", true);
        
        if(pm.getDirectionX() == -1)
            sr.flipX = true;
        if (pm.getDirectionX() == 1)
            sr.flipX = false;

    }
}
