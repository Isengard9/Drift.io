using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;
    public float speed = 1.0f;
    public float swayAmount = 1.0f;
    public float swaySpeed = 1.0f;

    private Vector3 offset;
    private float swayTimer = 0;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        //transform.position = target.position + offset;

        swayTimer += Time.deltaTime * swaySpeed;
        transform.RotateAround(target.position, target.up, Mathf.Sin(swayTimer) * swayAmount);
    }
}