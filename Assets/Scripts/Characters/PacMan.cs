using UnityEngine;

// PacMan class that recieves the player input 
// and controls PacMan's movement
[RequireComponent(typeof(Movement))]
public class PacMan : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Movement movement;

    public bool energyzed;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.rotation = movement.direction.y == 0 ? 
            Quaternion.identity : Quaternion.Euler(0, 0, 90);

        sprite.flipX = (movement.direction.x == -1 || movement.direction.y == -1);
    }

    #region INPUTS

    public void OnUp()
    {
        movement.SetDirection(Vector2.up);
    }

    public void OnDown()
    {
        movement.SetDirection(Vector2.down);
    }

    public void OnLeft()
    {
        movement.SetDirection(Vector2.left);
    }

    public void OnRight()
    {
        movement.SetDirection(Vector2.right);
    }

    #endregion
}
