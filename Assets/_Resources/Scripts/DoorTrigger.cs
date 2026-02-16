using _Resources.Scripts;
using UnityEngine;

public class DoorTrigger : TriggerEvent
{
    [SerializeField] private string levelToLoad;
    [SerializeField] private float delay;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneController sceneController = FindFirstObjectByType<SceneController>();
            sceneController.LoadLevelByName(levelToLoad, delay);
            OnEnter.Invoke();
        }
    }
}
