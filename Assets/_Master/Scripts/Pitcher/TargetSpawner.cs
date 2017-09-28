using UnityEngine;

/// <summary>
/// This class handles the spawning of targets for the pitching game mode.
/// </summary> 
[RequireComponent(typeof(AudioSource))]
public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject target; // Type of target that is spawned

    [SerializeField]
    private float spawnTime = 5.0f; // Time that must pass before a new target is spawned
    private float timer;

    [SerializeField]
    private AudioClip spawnSound;

    private GameObject targetRef = null; // Reference to newly spawned target. When it is null, a new target can be spawned.
    private AudioSource audSrc;

    private void Start()
    {
        timer = spawnTime;

        audSrc = this.GetComponent<AudioSource>();

        if (spawnSound)
        {
            audSrc.clip = spawnSound;
        }
    }
    
    private void FixedUpdate()
    {
        // If no target has spawned, subtract from the timer then spawn a new target
        if (!targetRef)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (target)
                {
                    GameObject newTarget = Instantiate(target, this.transform.position, this.transform.rotation);
                    targetRef = newTarget;
                    timer = spawnTime;

                    if (spawnSound)
                    {
                        audSrc.Play();
                    }
                }
            }
        }
    }
}