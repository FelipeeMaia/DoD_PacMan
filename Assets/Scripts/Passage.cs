using UnityEngine;

public class Passage : MonoBehaviour
{
    [SerializeField] private Transform passageDestination;

    // Script that let the characters teleport
    // from one side of the map to the other
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        other.transform.position = passageDestination.position;
    }
}
