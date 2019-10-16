using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handController : MonoBehaviour
{
    Rigidbody2D rb = null;

    public Sprite sprite1;
    public Sprite sprite2;

    public float cameraSpeed = 0.2f;
    public float pullStrength = 30;

    public Transform pivot;

    float x_pos = 0;
    float y_pos = 0;

    float horLimit = 5;
    float verLimit = 7;

    Vector2 nextPos;

    public float backgroundWidth = 106.6f;
    public GameObject rightClone;
    public GameObject leftClone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X");
        float moveY = Input.GetAxis("Mouse Y");

        float nextX = x_pos + moveX;
        float nextY = y_pos + moveY;

        if (Mathf.Abs(nextX) < verLimit)
        {
            transform.position += new Vector3(moveX, 0, 0);
            x_pos += moveX;
        }
        if (Mathf.Abs(nextY) < horLimit)
        {
            transform.position += new Vector3(0, moveY, 0);
            y_pos += moveY;
        }
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            if (rb != null)
            {
                if (Input.GetMouseButtonUp(0) || !DistanceCheck(rb.GetComponent<Transform>()))
                {
                    rb = null;
                    GetComponent<SpriteRenderer>().sprite = sprite1;
                    rightClone.GetComponent<SpriteRenderer>().sprite = sprite1;
                    leftClone.GetComponent<SpriteRenderer>().sprite = sprite1;
                }
                else
                {
                    rb.AddForce(new Vector2(pivot.position.x - rb.position.x, pivot.position.y - rb.position.y) * pullStrength);
                }
            }
        }

        if (transform.position.x > backgroundWidth / 2)
        {
            transform.position -= new Vector3(backgroundWidth, 0, 0);
        }
        else if (transform.position.x < -backgroundWidth / 2)
        {
            transform.position += new Vector3(backgroundWidth, 0, 0);
        }

        leftClone.transform.position = transform.position + new Vector3(-backgroundWidth, 0, 0);
        rightClone.transform.position = transform.position + new Vector3(backgroundWidth, 0, 0);

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(cameraSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-cameraSpeed, 0, 0);
        }

        if (transform.position.x > backgroundWidth / 2)
        {
            transform.position -= new Vector3(backgroundWidth, 0, 0);
        }
        else if (transform.position.x < -backgroundWidth / 2)
        {
            transform.position += new Vector3(backgroundWidth, 0, 0);
        }
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        BlendItem item = collision.GetComponent<BlendItem>();
        if (collision.transform.parent != null)
        {
            item = collision.GetComponentInParent<BlendItem>();
        }

        if (item != null || collision.tag == "Lid")
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<SpriteRenderer>().sprite = sprite2;
                rightClone.GetComponent<SpriteRenderer>().sprite = sprite2;
                leftClone.GetComponent<SpriteRenderer>().sprite = sprite2;
                rb = collision.GetComponent<Rigidbody2D>();
                if (collision.transform.parent != null)
                {
                    rb = collision.GetComponentInParent<Rigidbody2D>();
                }
            }
        }
    }

    public bool DistanceCheck(Transform rigidbody)
    {
        bool result = true;

        Vector2 distance = new Vector2(rigidbody.position.x - pivot.position.x, rigidbody.position.y - pivot.position.y);
        float disLen = Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2));
        if (disLen > 6) result = false;

        return result;
    }

    public void Release()
    {
        rb = null;
        GetComponent<SpriteRenderer>().sprite = sprite1;
        rightClone.GetComponent<SpriteRenderer>().sprite = sprite1;
        leftClone.GetComponent<SpriteRenderer>().sprite = sprite1;
    }
    
}
