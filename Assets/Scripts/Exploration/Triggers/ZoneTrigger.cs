using UnityEngine;

/// <summary>
/// Represents a trigger that can enable / disable zones
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 dotAngle;
    [SerializeField] private GameObject zoneFront;
    [SerializeField] private GameObject zoneBack;

    private bool shouldCheck;

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag.Equals("Player"))
        {
            shouldCheck = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellowNice;
        Gizmos.DrawLine(transform.position,transform.position+dotAngle*2f);
    }


    void Update()
    {
        if (shouldCheck)
        {
            shouldCheck = false;
            float dot = Vector3.Dot(dotAngle,Player.instance.GetBody().linearVelocity);
            print(dot);

            zoneBack.SetActive(dot < 0);
            zoneFront.SetActive(dot >= 0);
        }
    }
}
