using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public GameObject fireball;
    public Transform fireBallPoint;
    public float fireBallSpeed = 600;
    public void FireBallAttack()
    {
        GameObject ball = Instantiate(fireball, fireBallPoint.position, Quaternion.identity);

        ball.GetComponent<Rigidbody>().AddForce(fireBallPoint.forward * fireBallSpeed);

    }
}
