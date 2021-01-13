using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float g;
    void Start()
    {
        Physics.gravity = Vector3.down * g;
    }
}
