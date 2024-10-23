using UnityEngine;

public class ActionWander : FSMAction
{
    [Header("Config")]
    [SerializeField] private bool useDebug;
    [SerializeField] private bool useRandomMovement;

    [Header("Values")]
    [SerializeField] private float wanderSpeed;
    [SerializeField] private Vector2 moveRange;
    [SerializeField] private float minDistanceCheck = 0.5f;

    [Header("Obstacles")]
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float rangeDetection;
    
    private Vector3 movePosition;
    private Vector3 moveDirection;

    private void Start() 
    {
        GetNewMovePosition();    
    }

    public override void Act()
    {
        moveDirection = (movePosition - transform.position).normalized;
        transform.Translate(moveDirection * (wanderSpeed * Time.deltaTime));
    }

    private void GetNewMovePosition()
    {
        if (useRandomMovement)
        {
            movePosition = transform.position + GetRandomDirection();
        }
    }

    private Vector3 GetRandomDirection()
    {
        float randomX = Random.Range(-moveRange.x, moveRange.x);
        float randomY = Random.Range(-moveRange.y, moveRange.y);
        return new Vector3(randomX, randomY);
    }

    private void OnDrawGizmos() 
    {
        if (useDebug == false) return;

        if (useRandomMovement)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, moveRange * 2f);
        }

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, movePosition);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeDetection);
    }
}