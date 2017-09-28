using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ScoreZone : MonoBehaviour
{
    [SerializeField]
    private float scoreModifier = 1.0f;

    private GamestateManager gsManager;

    private void Start()
    {
        this.GetComponent<BoxCollider>().isTrigger = true;

        gsManager = GameObject.FindGameObjectWithTag("GamestateManager").GetComponent<GamestateManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ball")
        {
            gsManager.ScoreModifier += scoreModifier;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(this.transform.position, this.GetComponent<BoxCollider>().size);
    }
}