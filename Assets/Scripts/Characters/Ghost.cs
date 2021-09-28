using UnityEngine;

// Ghost class that controls it's movement and behavior
[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    [SerializeField] private int value;

    private PacMan target;

    private Movement movement;

    private void Awake()
    {
        target = FindObjectOfType<PacMan>();
        movement = GetComponent<Movement>();
    }

    /* 
     * if(pacman.energyzd) runaway
     * else follow
     */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(target.energyzed)
        {
            ScoreManager.instance.OnScored(value);
            AudioManager.instance.PlayClip("Ghost");
            //dies and returns to cell
        }
        else
        {
            GameManager.instance.LoseLife();
            AudioManager.instance.PlayClip("Death");
        }
    }
}
