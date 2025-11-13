using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    private Rigidbody2D currentObject;

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosInWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        if (Input.GetMouseButtonUp(0))
        {
            if (currentObject) currentObject = null;
        }

        if (Input.GetMouseButtonDown(0))
        {

            Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosInWorld, 0.1f);
            foreach(Collider2D collider in colliders)
            {
                if(collider.attachedRigidbody && collider.attachedRigidbody.tag == "Player")
                {
                    currentObject = collider.attachedRigidbody;
                    break;
                }
            }
        }

        if (currentObject)
        {
            currentObject.MovePosition(mousePosInWorld);
            currentObject.linearVelocity = Vector2.zero;
        }
    }
}
