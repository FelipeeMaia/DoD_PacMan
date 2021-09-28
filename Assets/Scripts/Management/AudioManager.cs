using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that manages audiosources and what to play on them
public class AudioManager : MonoBehaviour
{
    #region SINGLETON PATTERN

    private static AudioManager _instance;

    public static AudioManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<AudioManager>();
                }
            }

            return _instance;
        }
    }

    #endregion

    [SerializeField] private int sourceQuantity;

    private AudioLibrary library;
    private Queue<AudioSource> sourcePool;

    private AudioSource sirenSource;

    private void Awake()
    {
        library = FindObjectOfType<AudioLibrary>();
    }

    // In the start we create several audiosources,
    // and save them in a pool to reuse constantly
    private void Start()
    {
        sourcePool = new Queue<AudioSource>();
        GameObject _go;

        for (int i = 0; i < sourceQuantity; i++)
        {
            _go = new GameObject("Sound");
            AudioSource _as = _go.AddComponent<AudioSource>();

            _go.transform.parent = this.transform;
            _go.SetActive(false);

            sourcePool.Enqueue(_as);
        }

        _go = new GameObject("Sound");
        sirenSource = _go.AddComponent<AudioSource>();
        sirenSource.loop = true;
    }

    // Search for a audio clip in the library and plays it
    public void PlayClip(string clipName)
    {
        AudioSource _as = sourcePool.Dequeue();
        _as.gameObject.SetActive(true);

        ClipInfo _ci = library.SearchClip(clipName);

        _as.loop = _ci.loop;

        _as.clip = _ci.clip;
        _as.Play();

        if (_ci.duration == 0)
            _ci.duration = _ci.clip.length;

        StartCoroutine(EnqueueSoruce(_as, _ci.duration));
    }

    // Same as above, but the sirens have an exclusive AudioSource
    public void PlaySiren(int number = 1)
    {
        ClipInfo _ci = library.SearchClip("Siren_" + number);

        sirenSource.clip = _ci.clip;
        sirenSource.Play();
    }

    // After the clip duration ends, 
    // the audiosource is reinserted in the pool
    private IEnumerator EnqueueSoruce(AudioSource _as, float length)
    {
        yield return new WaitForSeconds(length + 0.1f);

        _as.gameObject.SetActive(false);
        sourcePool.Enqueue(_as);
    }
}
