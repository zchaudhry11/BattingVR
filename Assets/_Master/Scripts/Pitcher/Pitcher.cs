using UnityEngine;

/// <summary>
/// This class contains all of the logic associated with the NPC ball pitcher.
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class Pitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private GameObject hand;

    [SerializeField]
    private float throwTimer = 4.0f;

    [SerializeField]
    private Vector3 ballForce = new Vector3(0, 0, 950.0f);

    [SerializeField]
    private AudioClip launchSound;

    private Rigidbody ballRB;
    private float timer = 1.0f;
    private AudioSource audSrc;

    private void Start ()
    {
		if (ball)
        {
            ballRB = ball.GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("The ball could not be found!");
        }

        audSrc = this.GetComponent<AudioSource>();

        if (launchSound)
        {
            audSrc.clip = launchSound;
        }
    }

    private void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (ball && hand)
            {
                // Reset ball
                ball.transform.position = hand.transform.position;
                ballRB.velocity = Vector3.zero;

                // Throw ball and apply a random force to the ball.
                ballRB.AddForce(ballForce);
                //ballRB.AddForce(ballForce + GenerateRandomForce());

                audSrc.Play();
            }

            timer = throwTimer;
        }
    }

    /// <summary>
    /// Generates a random force to be added to the ball throw.
    /// </summary>
    private Vector3 GenerateRandomForce()
    {
        return new Vector3(Random.Range(-40.0f, 100.0f), Random.Range(0.0f, 100.0f), Random.Range(0.0f, 400.0f));
    }
}