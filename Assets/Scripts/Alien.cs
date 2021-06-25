using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    public GameObject planet;
    public GameObject player;
    private float gravity = 100;
    private bool onGround = false;
    private float distanceToGround;
    private Vector3 groundNormal;
    private Rigidbody rb;
    float moveSpeed=3.0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        Movement();
        GroundControl();
        GravityControl();
        Rotation();
    }

    void Movement()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void GroundControl()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {
            distanceToGround = hit.distance;
            groundNormal = hit.normal;
            onGround = (distanceToGround <= 0.5f);
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
        Quaternion groundRotation = Quaternion.FromToRotation(transform.up, groundNormal);
        Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = groundRotation * Quaternion.Slerp(transform.rotation, lookRotation, 3 * Time.deltaTime);
    }
}
