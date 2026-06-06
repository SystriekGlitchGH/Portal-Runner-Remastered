using UnityEngine;

public class ParticleDetach : MonoBehaviour
{
    public ParticleSystem ps;

    public void Detach()
    {
        transform.parent = null;
        ps.Stop(true);
    }
}
