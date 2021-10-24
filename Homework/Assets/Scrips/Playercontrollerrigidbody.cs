using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontrollerrigidbody : MonoBehaviour
{
    public float speed = 2f;
    Rigidbody rb;
    float newRotY = 0;
    public float rotSpeed = 20f;
    public GameObject prefabBullet;
    public Transform gunPosition;
    public float gunPower = 15f;
    public float gunCooldown = 2f;
    public float gunCooldownCount = 0;
    public bool hasGun = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(0, 0, speed,ForceMode.VelocityChange);
            newRotY = 0;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(0, 0, -speed,ForceMode.VelocityChange);
            newRotY = 180;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(speed, 0, 0,ForceMode.VelocityChange);
            newRotY = 90;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-speed, 0, 0,ForceMode.VelocityChange);
            newRotY = -90;
        }

        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, newRotY, 0),
                                             transform.rotation,
                                             rotSpeed = Time.deltaTime
                                            );
    }
    private void Update()
    {
        gunCooldownCount += Time.deltaTime;
        //ยิงปืน
        if (Input.GetKeyDown(KeyCode.LeftControl) && hasGun && gunCooldownCount >= gunCooldown)
        {
            gunCooldownCount = 0;
            GameObject bullet = Instantiate(prefabBullet, gunPosition.position, gunPosition.rotation);
           
            Rigidbody bRb = bullet.GetComponent<Rigidbody>();
            bRb.AddForce(transform.forward * gunPower, ForceMode.Impulse);

            Destroy(bullet, 2f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Point")
        {
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ponit")
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.name == "Gun")
        {
            print("Yeah!!! I have a gun!");
            Destroy(other.gameObject);
            hasGun = true;
        }
    }
}
