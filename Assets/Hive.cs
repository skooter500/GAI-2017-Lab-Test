using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour {

    public float polen = 10;
    public GameObject beePrefab;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnBees());
	}

    System.Collections.IEnumerator SpawnBees()
    {
        while (true)
        {
            GameObject[] bees = GameObject.FindGameObjectsWithTag("bee"); 
            if (polen >= 5 && bees.Length < 10)
            {
                polen -= 5;
                GameObject bee = Instantiate(beePrefab);
                bee.transform.position = transform.position;
                bee.transform.parent = transform;
            }            
            yield return new WaitForSeconds(2.0f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
