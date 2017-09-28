using UnityEngine;
using VRTK;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private GameObject rHand;

    [SerializeField]
    private GameObject lHand;

    private BallController ballController;

    private void Awake()
    {
        if (!ball)
        {
            Debug.LogError("No ball specified.");
        }

        if (!lHand)
        {
            Debug.LogError("No left hand specified.");
        }

        if (!rHand)
        {
            Debug.LogError("No right hand specified.");
        }
    }

    private void Start()
    {
        ballController = GameObject.FindGameObjectWithTag("Player").GetComponent<BallController>();
    }

    private void Update()
    {
        if (ballController.VR_RightController.gripPressed)
        {
            ball.transform.position = rHand.transform.position;
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (ballController.VR_LeftController.gripPressed)
        {
            ball.transform.position = lHand.transform.position;
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}