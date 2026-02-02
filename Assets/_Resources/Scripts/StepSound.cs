using UnityEngine;

public class StepSound : MonoBehaviour
{
    [SerializeField] private GroundSO[] grounds;
    [SerializeField] private float stepVolume = 1f;

    private AudioSource _audioSource;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayStepSound()
    {
        if (_playerMovement.groundColliders.Length <= 0)
            return;

        string tag = _playerMovement.groundColliders[0].tag;

        foreach (var ground in grounds)
        {
            if (ground.groundTag == tag)
            {
                _audioSource.PlayOneShot(ground.GetRandomClip(), stepVolume);
                return;
            }
        }
    }
}
