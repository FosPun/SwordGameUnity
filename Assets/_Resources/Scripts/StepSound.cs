using UnityEngine;

public class StepSound : MonoBehaviour
{
    [SerializeField] private GroundSO[] grounds;
    [SerializeField] private float stepVolume = 1f;
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private float groundCheckRadius = 1f;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayStepSound()
    {
        Collider[] groundColliders = Physics.OverlapSphere(new Vector3
            (
                transform.position.x, transform.position.y - groundCheckDistance, transform.position.z),
            groundCheckRadius,
            LayerMask.GetMask("Ground")
        );
        if ( groundColliders.Length <= 0)
            return;

        string tag = groundColliders[0].tag;

        foreach (var item in grounds)
        {
            if (item.groundTag == tag)
            {
                _audioSource.PlayOneShot(item.GetRandomClip(), stepVolume);
                return;
            }
        }
    }
}
