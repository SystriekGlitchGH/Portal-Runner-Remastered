using UnityEngine;

public class CameraLock : MonoBehaviour
{
    public Transform playerTransform;

    public bool isVertical;
    public float yAxisLock;
    public float xAxisLock;
    public float point1;
    public float point2;

    public float moveSpeed;

    public void FixedUpdate()
    {
        if (isVertical)
        {
            if(playerTransform.position.y < point1)
            {
                transform.position = new Vector3(xAxisLock, Mathf.Lerp(transform.position.y, point1, Time.fixedDeltaTime * moveSpeed), -10);
                return;
            }
            if (playerTransform.position.y > point2)
            {
                transform.position = new Vector3(xAxisLock, Mathf.Lerp(transform.position.y, point2, Time.fixedDeltaTime * moveSpeed), -10);
                return;
            }
            if (playerTransform.position.x != xAxisLock)
            {
                transform.position = new Vector3(xAxisLock, Mathf.Lerp(transform.position.y, playerTransform.position.y, Time.fixedDeltaTime * moveSpeed), -10);
            }
        }
        else
        {
            if (playerTransform.position.x < point1)
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, point1, Time.fixedDeltaTime * moveSpeed), yAxisLock, -10);
                return;
            }
            if (playerTransform.position.x > point2)
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, point2, Time.fixedDeltaTime * moveSpeed), yAxisLock, -10);
                return;
            }
            if (playerTransform.position.y != yAxisLock)
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, playerTransform.position.x, Time.fixedDeltaTime * moveSpeed), yAxisLock, -10);
        }
    }
}
