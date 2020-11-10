using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Programming Assignment 4: Asteroids
/// </summary>
public class Ship : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D TwoDComponent;

    //Thrust direction
    Vector2 thrustDirection;

    //Thrust force
    const int ThrustForce = 3;

    //Circle collider radius
    float circleColliderRadius;

    //Rotation constant
    const int RotateDegreesPerSecond = 50;

    // Start is called before the first frame update
    void Start()
    {
        TwoDComponent = GetComponent<Rigidbody2D>();
        circleColliderRadius = GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        float rotationInput = Input.GetAxis("Rotate");
        float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
        if(rotationInput<0)
        {
            //rotationAmount *= -1;
            transform.Rotate(Vector3.forward, rotationAmount);
        }
        if(rotationInput>0)
        {
            rotationAmount *= -1;
            transform.Rotate(Vector3.forward, rotationAmount);
        }
    }

    //To add force to the ship
    void FixedUpdate()
    {
        float thrustInput = Input.GetAxis("Thrust");
        float ZAngle = transform.eulerAngles.z;
        ZAngle = Mathf.Deg2Rad * ZAngle;
        thrustDirection = new Vector2(Mathf.Cos(ZAngle),Mathf.Sin(ZAngle));
        if (thrustInput>0)
        {
            TwoDComponent.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
        }
    }

    //Wrapping the ship
    void OnBecameInvisible()
    {
        Vector2 position = transform.position;
        if(position.x +circleColliderRadius>ScreenUtils.ScreenRight)
        {
            position.x = ScreenUtils.ScreenLeft - circleColliderRadius;
        }
        else if(position.x - circleColliderRadius < ScreenUtils.ScreenLeft)
        {
            position.x = ScreenUtils.ScreenRight + circleColliderRadius;
        }
        if(position.y + circleColliderRadius > ScreenUtils.ScreenTop)
        {
            position.y = ScreenUtils.ScreenBottom - circleColliderRadius;
        }
        else if(position.y - circleColliderRadius < ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenTop + circleColliderRadius;
        }
        transform.position = position;
    }
}
