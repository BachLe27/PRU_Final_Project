using Assets.Scripts;
using System;
using UnityEngine;

public class Gold : MonoBehaviour, ICollectible
{
    public static event Action OnGoldCollected;
    Rigidbody2D rb;
    [SerializeField] public float speed;

    bool hasTarget;
    Vector3 targetPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Collect()
    {
        Debug.Log("Gold Collected");
        Destroy(gameObject);
        OnGoldCollected?.Invoke();
    }

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * speed;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }
}
