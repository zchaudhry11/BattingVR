using UnityEngine;

/// <summary>
/// This class handles the scoring for when a ball hits a target.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PitcherScore : MonoBehaviour
{
    [SerializeField]
    private int scoreValue = 5;

    [SerializeField]
    private AudioClip scoreSound;

    private bool destroy = false;
    private float destroyTimer = 1.0f;

    private GamestateManager gsManager;
    private AudioSource audSrc;

    private void Start()
    {
        this.GetComponent<BoxCollider>().isTrigger = true;

        gsManager = GameObject.FindGameObjectWithTag("GamestateManager").GetComponent<GamestateManager>();
        audSrc = this.GetComponent<AudioSource>();

        if (scoreSound)
        {
            audSrc.clip = scoreSound;
        }
    }

    private void FixedUpdate()
    {
        // If target is hit with a ball then shrink the target and then destroy it
        if (destroy)
        {
            if (destroyTimer > 0)
            {
                destroyTimer -= Time.deltaTime;

                if (destroyTimer > 0.1f)
                {
                    this.transform.localScale *= destroyTimer;
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        // If a target gets hit by the ball
        if (col.tag == "Ball")
        {
            gsManager.CalculatePitchingScore(scoreValue);

            if (scoreSound)
            {
                audSrc.Play();
            }

            destroy = true;
        }
    }
}