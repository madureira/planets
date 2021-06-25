using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject planet;
    public float speed = 4;
    private float gravity = 100;
    private bool onGround = false;
    private float distanceToGround;
    private Vector3 groundNormal;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        Movement();
        LocalRotation();
        GroundControl();
        GravityControl();
        Rotation();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(x, 0, z);
    }

    void LocalRotation()
    {
        float rotationAngle = 150 * Time.deltaTime;

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotationAngle, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotationAngle, 0);
        }
    }

    void GroundControl()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {
            distanceToGround = hit.distance;
            groundNormal = hit.normal;
            onGround = (distanceToGround <= 0.2f);
        }
    }

    void GravityControl()
    {
        Vector3 gravityDirection = (transform.position - planet.transform.position).normalized;

        if (!onGround)
        {
            rb.AddForce(gravityDirection * -gravity);
        }
    }

    void Rotation()
    {
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;
        transform.rotation = toRotation;
    }
}