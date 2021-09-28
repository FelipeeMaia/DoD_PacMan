using UnityEngine;
using System.Collections;

//EEnergyzer class, when eaten, energyze the pacman for a duration
public class Energyzer : Dot
{
    public float energyDuration;
    private PacMan pacman;

    protected override void Eated()
    {
        base.Eated();

        if (pacman)
            StartCoroutine(Energyze(energyDuration));
    }

    private IEnumerator Energyze(float duration)
    {
        pacman.energyzed = true;
        yield return new WaitForSeconds(duration);
        pacman.energyzed = false;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        pacman = other.GetComponent<PacMan>();

        base.OnTriggerEnter2D(other);
    }
}
