using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    void Start()
    {
        // set the rotation of the coin
        // add random rotation X
        transform.Rotate(new Vector3(UnityEngine.Random.Range(0, 360), 0, 0));
    }



    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(60, 0, 0) * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.instance.addCoin();
            Destroy(gameObject);
        }
    }
}
