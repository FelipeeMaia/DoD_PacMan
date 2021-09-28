using UnityEngine;

//Dot class, to be eaten and reclaim points
public class Dot : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Collider2D col;

    [Header("Variables")]
    public int value;
    public string audioKey;

    private void Start()
    {
        GameManager.instance.OnHardReset += ResetState;

        ResetState();
    }

    public void ResetState()
    {
        sprite.enabled = true;
        col.enabled = true;
    }

    protected virtual void Eated()
    {
        ScoreManager.instance.OnScored(value);
        GameManager.instance.OnDotEated();
        AudioManager.instance.PlayClip(audioKey);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Eated();

        sprite.enabled = false;
        col.enabled = false;
    }
}
