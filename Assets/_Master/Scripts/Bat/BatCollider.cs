using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCollider : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 2.0f)]
    private float velocityMultiplier = 1.0f;

    [SerializeField]
    private float resetTimer = 0.25f;

    private bool hitBall = false;
    private float timer = 0;

    [SerializeField]
    private AudioClip batSound;

    private AudioSource audSrc;

    private void Start()
    {
        audSrc = this.transform.parent.GetComponent<AudioSource>();

        if (batSound)
        {
            audSrc.clip = batSound;
        }
    }

    private void Update()
    {
        Vector3 fwd = this.transform.position;
        fwd.z += 1;

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, fwd, out hit, 0.25f))
        {
            if (hit.transform.gameObject.tag == "Ball")
            {
                if (!hitBall)
                {
                    hitBall = true;
                    timer = resetTimer;

                    hit.transform.gameObject.GetComponent<Rigidbody>().velocity *= velocityMultiplier;
                    audSrc.Play();
                    print("hit");
                }
            }
            //Debug.DrawLine(this.transform.position, fwd, Color.red, 2.0f);
        }
    }

    private void FixedUpdate()
    {
        if (hitBall)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                hitBall = false;
            }
        }
    }
}