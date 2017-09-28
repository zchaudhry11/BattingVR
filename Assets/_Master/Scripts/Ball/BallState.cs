using UnityEngine;

public class BallState : MonoBehaviour
{
    public float DistTraveled { get; set; }

    [SerializeField]
    private AudioClip batSound;

    private bool touchedBat = false;
    private bool trackPos = true;

    private Vector3 startPos;
    private Vector3 endPos;

    private AudioSource audSrc;

    private void Start()
    {
        audSrc = GameObject.FindGameObjectWithTag("Bat").GetComponent<AudioSource>();

        if (audSrc)
        {
            if (batSound)
            {
                audSrc.clip = batSound;
            }
        }
    }

    private void Update()
    {
        if (trackPos)
        {
            // If the ball hasn't been hit by the bat yet
            if (!touchedBat)
            {
                startPos = this.transform.position;
            }
            else
            {
                // When the hit ball touches the ground, get its final position
                if (this.transform.position.y <= 0 || this.transform.position == Vector3.zero)
                {
                    endPos = this.transform.position;
                    trackPos = false;
                    CalculateDistance();
                }
            }
        }
    }

    /// <summary>
    /// Calculates the distance between when the bat was hit and where it landed.
    /// </summary>
    private void CalculateDistance()
    {
        DistTraveled = Vector3.Distance(startPos, endPos);
        touchedBat = false;
        trackPos = true;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (!touchedBat)
        {
            if (col.gameObject.tag == "Bat" || col.gameObject.tag == "BatCollider")
            {
                touchedBat = true;
                audSrc.Play();
            }
        }

        // Stop tracking distance if ball hits the ground or any part of the environment
        if (col.gameObject.tag == "Environment")
        {
            endPos = this.transform.position;
            trackPos = false;
            CalculateDistance();
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (!touchedBat)
        {
            if (col.gameObject.tag == "Bat" || col.gameObject.tag == "BatCollider")
            {
                touchedBat = true;
                audSrc.Play();
            }
        }
    }
}