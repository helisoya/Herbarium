using UnityEngine;

public class ForagingInput : MonoBehaviour
{
    private ForagingPickable currentObject;
    [SerializeField] private float itemSpeed = 5f;
    [SerializeField] private float rotateSpeed = 15f;
    [SerializeField] private float cutForwardLength = 1f;
    [SerializeField] private LayerMask mask;

    [Header("Plant")]
    [SerializeField] private Joint2D[] plantJoints;
    [SerializeField] private int plantHP;

    void OnDrawGizmosSelected()
    {
        if (currentObject)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(currentObject.transform.position, currentObject.transform.position + currentObject.transform.up * cutForwardLength);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosInWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        if (Input.GetMouseButtonUp(1))
        {
            if (currentObject)
            {
                currentObject.Drop();
                currentObject = null;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(currentObject && currentObject.CanCut())
            {
                bool cutFound = false;
                RaycastHit2D[] hits = Physics2D.RaycastAll(currentObject.transform.position, currentObject.transform.up, cutForwardLength, mask);
                foreach (RaycastHit2D hit in hits)
                {
                    print(hit.rigidbody.gameObject);
                    if (hit.rigidbody.gameObject.TryGetComponent<ForagingCutPoint>(out ForagingCutPoint cutPoint))
                    {
                        cutPoint.Cut();
                        cutFound = true;
                        break;
                    }
                }

                if (!cutFound && hits.Length > 0)
                {
                    // Unless you can interact with a non plant rigidbody (the dropped flower for instance), everything will be fine ?
                    plantHP--;
                    if(plantHP <= 0)
                    {
                        foreach (Joint2D joint in plantJoints) joint.enabled = false;
                    }
                    print("You damaged the plant : (");
                }

            }
        }

        if (Input.GetMouseButtonDown(1))
        {

            Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosInWorld, 0.1f);
            foreach(Collider2D collider in colliders)
            {
                if(collider.attachedRigidbody && collider.attachedRigidbody.tag == "Player" && collider.attachedRigidbody.TryGetComponent<ForagingPickablePart>(out ForagingPickablePart obj))
                {
                    ForagingPickable parent = obj.GetParent();
                    if (parent.CanBePickedUp())
                    {
                        parent.Pickup(obj);
                        currentObject = parent;
                        break;
                    }
                }
            }
        }

        if (currentObject)
        {
            
            float distance = Vector2.Distance(currentObject.transform.position, mousePosInWorld);
            Vector2 direction = mousePosInWorld - new Vector2(currentObject.transform.position.x, currentObject.transform.position.y);

            if (distance >= 0.1f && currentObject.CanRotate())
            {
                currentObject.RotateTowards(direction.normalized,rotateSpeed);
            }

            currentObject.MoveTowards(mousePosInWorld, itemSpeed);

            return;
        }
    }
}
