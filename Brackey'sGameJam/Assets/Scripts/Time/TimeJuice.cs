using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeJuice : MonoBehaviour
{
    public bool isRewinding = false; //checks if we are in the act of rewinding
    public float recordTime = 2f; //how long the script records the players actions from however long ago
    public float juiceAmount = 100;
    public float juiceCost = 20;

    List<Vector3> positions; //gets player information

    public Rigidbody rb; // gets player's rigidbody

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector3>();
        //rb.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (juiceAmount >= juiceCost)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                StartRewind();
            juiceAmount = juiceAmount - juiceCost;
            //if (Input.GetKeyUp(KeyCode.Q))
            //StopRewind();
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding == true)
            Rewind();
        else
            Record();
    }

    void Rewind()
    {
        if (positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if(positions.Count > Mathf.Round((recordTime/ Time.fixedDeltaTime)))
        {
            positions.RemoveAt(positions.Count - 1);
        }
        positions.Insert(0, transform.position);
    }

    void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
        Time.timeScale = 3.0f;
    }

    void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
        Time.timeScale = 1.0f;
    }
}
