using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {
    public float value = 0;
	// Use this for initialization
	void Start () {
        value = Random.Range(1, 5);
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = Vector3.Lerp(
            transform.localScale, new Vector3(1, value, 1), Time.deltaTime);
        Vector3 pos = transform.position;
        pos.y = transform.localScale.y / 2;
        transform.position = pos;
	}
}
