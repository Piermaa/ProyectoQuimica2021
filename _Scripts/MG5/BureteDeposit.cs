using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BureteDeposit : MonoBehaviour
{

    public ColoredSubstance _coloredSubstance;
    private MiniGame5Controller _miniGame5Controller;
    [SerializeField] GameObject blue, red, orange;
    // Start is called before the first frame update
    void Start()
    {
        _coloredSubstance = FindObjectOfType<ColoredSubstance>();
        _miniGame5Controller = FindObjectOfType<MiniGame5Controller>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drop"))
        {

            if (_miniGame5Controller.actualDrop == _miniGame5Controller.rodaminDrop)
            {
                red.SetActive(true);
            }
            if (_miniGame5Controller.actualDrop == _miniGame5Controller.methylOrangeDrop)
            {
                orange.SetActive(true);
            }
            if (_miniGame5Controller.actualDrop == _miniGame5Controller.methyleneBlueDrop)
            {
                blue.SetActive(true);
            }
            Destroy(other.gameObject);


        }
    }
    /// <summary>
    /// Sets the objects to its default state
    /// </summary>
    public void DefaultState()
    {
        red.SetActive(false);
        blue.SetActive(false);
        orange.SetActive(false);

    }
}
