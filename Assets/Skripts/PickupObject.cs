using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PickupObject : MonoBehaviour 
{
    static bool triggerOn = false; 
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.Q;
    public KeyCode throwKey = KeyCode.Mouse0;
    public float pickupDistance = 1f;
    public Transform holdPosition;
    private RaycastHit2D hit;
    private GameObject heldObject;
    public GameObject item;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Table")
        {
          triggerOn = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerOn = false;
    }
    void Update()
    {
        
        if (Input.GetKeyDown(pickupKey))
        {
            Physics2D.queriesStartInColliders = false;
            hit = Physics2D.Raycast(transform.position, transform.right, pickupDistance);
            if (triggerOn)
            {
                item.GetComponent<CircleCollider2D>().enabled = false;
                item.GetComponent<Transform>().position = transform.position;
                heldObject = item;
                heldObject.transform.SetParent(holdPosition);
                heldObject.transform.localPosition = Vector2.zero;
                heldObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            else if (hit.collider != null && hit.collider.CompareTag("Pickup"))
            {
                item.GetComponent<CircleCollider2D>().enabled = false;
                // код для подбора объекта
                heldObject = hit.collider.gameObject;
                heldObject.transform.SetParent(holdPosition);
                heldObject.transform.localPosition = Vector2.zero;
                heldObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
        }
        
        if (Input.GetKeyDown(dropKey))
        {
            if (heldObject != null && triggerOn)
            {
                heldObject.transform.SetParent(null);
                heldObject.GetComponent<Rigidbody2D>().isKinematic = false;
                item.GetComponent<Transform>().position = new Vector2(-5.75f, 2.57f);
                heldObject = null;
                //item.GetComponent<CircleCollider2D>().enabled = true;
            }
            else if (heldObject != null && triggerOn == false)
            {
                heldObject.transform.SetParent(null);
                heldObject.GetComponent<Rigidbody2D>().isKinematic = false;
                Rigidbody2D heldObjectRigidbody = heldObject.GetComponent<Rigidbody2D>();
                heldObjectRigidbody.velocity = new Vector2(transform.localScale.x * 2, 0f);
                heldObject = null;
                item.GetComponent<CircleCollider2D>().enabled = true;

            }
        }
        if (Input.GetKeyDown(throwKey))
        {
            if (heldObject != null)
            {
                heldObject.transform.SetParent(null);
                heldObject.GetComponent<Rigidbody2D>().isKinematic = false;
                Vector3 throwDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - heldObject.transform.position;
                heldObject.GetComponent<Rigidbody2D>().AddForce(throwDirection * 40f, ForceMode2D.Impulse);
                heldObject = null;
            }
        }
    }
}
