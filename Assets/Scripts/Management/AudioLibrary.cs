using System.Collections.Generic;
using UnityEngine;

// This class is responsibl for organize 
// all clips that will be used in the game
public class AudioLibrary : MonoBehaviour
{ 
    public ClipInfo[] _clips;

    private Dictionary<string, ClipInfo> Clips;

    // Reads the ClipInfo array and save it in a Dictionary
    private void Start()
    {
        Clips = new Dictionary<string, ClipInfo>();

        foreach(ClipInfo _ci in _clips)
        {
            Clips.Add(_ci.name, _ci);
        }
    }

    // Return a clip from the dictionary from the key recieved
    public ClipInfo SearchClip(string key)
    {
        ClipInfo _ci;
        bool error = !Clips.TryGetValue(key, out _ci);

        if (error)
            Debug.LogWarning("Clip don't found.");

        return _ci;
    }
}

// Struct for easily save the clips info
[System.Serializable]
public struct ClipInfo
{
    public string name;
    public AudioClip clip;
    public float duration;
    public bool loop;
}
