using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveErlenmeyers : MonoBehaviour
{
    int moves;
    public bool canMove;
    float flasksMovement= 0.2851318f;
    int maxMoves=2;
    Vector3 defaultPos = new Vector3(17.251f, 1.404f, -0.526f);
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        moves = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Moves the objects to the left
    /// </summary>
    public void MoveErlenmeyer()
    {
        if (moves < maxMoves && canMove)
        {
            Vector3 newPos = new Vector3(transform.position.x - flasksMovement, transform.position.y, transform.position.z);
            this.transform.SetPositionAndRotation(newPos, this.transform.rotation);
          
            moves++;
            canMove = false;
        }
    }
    /// <summary>
    /// Sets the objects to its default positions
    /// </summary>
    public void DefaultPositions()
    {

        this.transform.SetPositionAndRotation(defaultPos, this.transform.rotation);
        Debug.Log("sexo?");
        moves=0;
        canMove = true;
    }

}
