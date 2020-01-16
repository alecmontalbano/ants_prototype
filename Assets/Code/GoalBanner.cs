using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBanner : MonoBehaviour
{

    [SerializeField] private float bobSpeed = 20.0f;
    [SerializeField] private float bobHeight = 0.5f;

    [SerializeField] private float toFloorOffset = 5.0f;

    void Update()
    {
        Vector3 pos = transform.position;

        float newY = Mathf.Sin((Time.time * bobSpeed)) + toFloorOffset;
        transform.position = new Vector3(pos.x, newY * bobHeight, pos.z);
    }
}
