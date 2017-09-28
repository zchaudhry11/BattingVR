using UnityEngine;
using VRTK;

public class BallController : MonoBehaviour
{
    public GameObject VR_RightHand;
    public VRTK_ControllerEvents VR_RightController;
    public VRTK_InteractTouch VR_RightTouch;

    public GameObject VR_LeftHand;
    public VRTK_ControllerEvents VR_LeftController;
    public VRTK_InteractTouch VR_LeftTouch;

    private GameObject touchedObjRH;
    private GameObject touchedObjLH;
    private Vector3 touchedObjLastPosRH = Vector3.zero; // TODO: average last 3 frames' data
    private Vector3 touchedObjLastPosLH = Vector3.zero;

    private void Start()
    {
        VR_RightController = GameObject.FindGameObjectWithTag("VR_RightController").GetComponent<VRTK_ControllerEvents>();
        VR_RightTouch = GameObject.FindGameObjectWithTag("VR_RightController").GetComponent<VRTK_InteractTouch>();

        VR_LeftController = GameObject.FindGameObjectWithTag("VR_LeftController").GetComponent<VRTK_ControllerEvents>();
        VR_LeftTouch = GameObject.FindGameObjectWithTag("VR_LeftController").GetComponent<VRTK_InteractTouch>();

        // Subscribe to controller events TODO: left controller trigger release
        VR_RightController.TriggerReleased += new ControllerInteractionEventHandler(TriggerReleaseRH);
        VR_LeftController.TriggerReleased += new ControllerInteractionEventHandler(TriggerReleaseLH);

        // Subscribe to touch events
        VR_RightTouch.ControllerTouchInteractableObject += new ObjectInteractEventHandler(ObjectTouchedRH);
        VR_RightTouch.ControllerUntouchInteractableObject += new ObjectInteractEventHandler(ObjectReleasedRH);

        VR_LeftTouch.ControllerTouchInteractableObject += new ObjectInteractEventHandler(ObjectTouchedLH);
        VR_LeftTouch.ControllerUntouchInteractableObject += new ObjectInteractEventHandler(ObjectReleasedLH);
    }

    private void LateUpdate()
    {
        if (touchedObjRH)
        {
            touchedObjLastPosRH = touchedObjRH.transform.position;
        }

        if (touchedObjLH)
        {
            touchedObjLastPosLH = touchedObjLH.transform.position;
        }
    }

    private void Update()
    {
        if (touchedObjRH)
        {
            // Lock object into right hand
            if (VR_RightController.triggerPressed)
            {
                touchedObjRH.transform.position = VR_RightHand.transform.position;
                touchedObjRH.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        if (touchedObjLH)
        {
            // Lock object into left hand
            if (VR_LeftController.triggerPressed)
            {
                touchedObjLH.transform.position = VR_LeftHand.transform.position;
                touchedObjLH.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }

    /// <summary>
    /// Callback for when right trigger is released.
    /// </summary>
    private void TriggerReleaseRH(object sender, ControllerInteractionEventArgs e)
    {
        if (touchedObjRH)
        {
            Vector3 vel = touchedObjRH.transform.position - touchedObjLastPosRH;

            touchedObjRH.GetComponent<Rigidbody>().AddForce(vel.x * 100.0f, vel.y * 100.0f, vel.z * 100.0f, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Callback for when left trigger is released.
    /// </summary>
    private void TriggerReleaseLH(object sender, ControllerInteractionEventArgs e)
    {
        if (touchedObjLH)
        {
            Vector3 vel = touchedObjLH.transform.position - touchedObjLastPosLH;

            touchedObjLH.GetComponent<Rigidbody>().AddForce(vel.x * 100.0f, vel.y * 100.0f, vel.z * 100.0f, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Callback for when an interactable object is touched by the right hand.
    /// </summary>
    private void ObjectTouchedRH(object sender, ObjectInteractEventArgs e)
    {
        touchedObjRH = e.target;
    }

    /// <summary>
    /// Callback for when an interactable object is touched by the left hand.
    /// </summary>
    private void ObjectTouchedLH(object sender, ObjectInteractEventArgs e)
    {
        touchedObjLH = e.target;
    }

    /// <summary>
    /// Callback for when an interactable object is no longer touched by the right hand.
    /// </summary>
    private void ObjectReleasedRH(object sender, ObjectInteractEventArgs e)
    {
        touchedObjRH = null;
    }

    /// <summary>
    /// Callback for when an interactable object is no longer touched by the left hand.
    /// </summary>
    private void ObjectReleasedLH(object sender, ObjectInteractEventArgs e)
    {
        touchedObjLH = null;
    }
}