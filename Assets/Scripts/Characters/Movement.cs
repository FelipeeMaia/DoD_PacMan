using UnityEngine;

// Class used by PacMan and Ghosts for movement
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask labyrinthLayer;
    public bool ShowGizmos;

    public Vector2 direction;
    private Vector2 nextDirection;
    private Vector3 startingPosition;
    private bool movementLocked;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position; 
    }

    private void Start()
    {
        GameManager.instance.OnMildReset += ResetState;
        GameManager.instance.OnGameStart += UnlockMovement;

        ResetState();
    }

    public void ResetState()
    {
        direction = Vector2.zero;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        movementLocked = true;
    }

    // Keeps trying to change direction
    private void Update()
    {
        if (this.nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        Vector2 step = direction * speed * Time.fixedDeltaTime;

        if(!movementLocked)
            rb.MovePosition(position + step);
    }

    // Change the direction the character is moving
    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !IsOcupied(direction))
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }
        else
        {
            this.nextDirection = direction;
        }
    }

    // Checks if the direction it wants have a wall or not
    public bool IsOcupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position,
            Vector2.one * 1.8f, 0.0f, direction, 1.5f, labyrinthLayer);

        return hit.collider != null;
    }

    private void UnlockMovement()
    {
        movementLocked = false;
    }


    private void OnDrawGizmos()
    {
        if (!ShowGizmos) return;

        if (nextDirection != Vector2.zero)
        {
            Gizmos.color = Color.cyan;
            Vector3 cubPos = transform.position + (Vector3)nextDirection * 1.5f;
            Gizmos.DrawWireCube(cubPos, Vector3.one * 1.8f);
        }
    }
}