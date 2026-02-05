using UnityEngine;

[CreateAssetMenu(fileName = "GroundSO", menuName = "Scriptable Objects/GroundSO")]
public class GroundSO : ScriptableObject
{
    public string groundTag;
    public AudioClip[] clips;

    public AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
