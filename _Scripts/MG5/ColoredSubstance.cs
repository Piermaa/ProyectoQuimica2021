using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredSubstance : MonoBehaviour
{
    public int colorID;
    private MiniGame5Controller _miniGame5Controller;
    MoveErlenmeyers _moveErlenmeyers;
    Vector3 defaultPos= new Vector3(16.959f, 2.451707f, -0.498f);


    // Start is called before the first frame update
    void Start()
    {
        _miniGame5Controller = FindObjectOfType<MiniGame5Controller>();
        _moveErlenmeyers = FindObjectOfType<MoveErlenmeyers>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ValveExit"))
        {
            switch (colorID)
            {
                case 1:
                    SetDrop(_miniGame5Controller.methyleneBlueDrop, _miniGame5Controller.blueDropRatio);
                    break;

                case 2:
                    SetDrop(_miniGame5Controller.methylOrangeDrop, _miniGame5Controller.orangeDropRatio);
                    break;

                case 3:
                    SetDrop(_miniGame5Controller.rodaminDrop, _miniGame5Controller.rodaminDropRatio);
                    break;
             
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ValveExit"))
        {
            if (_miniGame5Controller.canDrop && _miniGame5Controller.valveIsOpen && _miniGame5Controller.actualDrop != _miniGame5Controller.eluentDrop)
            {
                _miniGame5Controller.SpawnDrop();
            }
          
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ValveExit"))
        {
            Debug.Log("SEXO");
            SetDrop(_miniGame5Controller.eluentDrop, _miniGame5Controller.blueDropRatio);
            this.gameObject.SetActive(false);
        
        }
    }

    /// <summary>
    /// Translates the colored substances to the bottom of the burete
    /// </summary>
    public void Elution()
    {
        if (_miniGame5Controller.valve.transform.localRotation.x >= _miniGame5Controller.closedValveValue)
        {

            _miniGame5Controller.rodamin.transform.Translate(Vector3.down * Time.deltaTime * _miniGame5Controller.eluentSpeed * _miniGame5Controller.rodaminSpeed);
            _miniGame5Controller.methyleneBlue.transform.Translate(Vector3.down * Time.deltaTime * _miniGame5Controller.eluentSpeed * _miniGame5Controller.methyleneBlueSpeed);
            _miniGame5Controller.methylOrange.transform.Translate(Vector3.down * Time.deltaTime * _miniGame5Controller.eluentSpeed * _miniGame5Controller.methylOrangeSpeed);

        }

    }

    /// <summary>
    /// Sets the actual type of drop
    /// </summary>
    /// <param name="dropObject">Object to insntantiate that emulates a drop</param>
    /// <param name="ratio">Time elapsed between instantiations</param>
    /// <returns>The actual game object that acts as drop</returns>
    public GameObject SetDrop(GameObject dropObject, float ratio)
    {
        _miniGame5Controller.actualDrop = dropObject;
        _miniGame5Controller.actualRatio = ratio;
        return _miniGame5Controller.actualDrop;
       
    }

    
    private void OnDisable()
    {
        _moveErlenmeyers.canMove = true;
    }


    /// <summary>
    /// Sets the object to its default state
    /// </summary>
    public void DefaultState()
    {
        transform.position = defaultPos;
    }

}
