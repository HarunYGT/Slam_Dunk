using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Power : MonoBehaviour
{
    [SerializeField] private float aci;
    [SerializeField] private float power;

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(aci, 90, 0) * power ,ForceMode.Force);
    }
}
