using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erlenmeyer : MonoBehaviour
{
    public Collider bocaDeErlenmeyer;
    public AudioSource device;
    public AudioSource drop;
    public GameObject acidSolution;
    public GameObject neutralizedSolution;
    public GameObject basicSolution;

    public float dropCount;
    public float naohml;
    public float acidCC;
    public float initialAcidCC;
    public float pH;
    public float naOHcc;
    public float protonsCc;
    public float protonsCuant;

    //intento 2
    public float dropCount2;
    public float mldeBase;
    public float molesdeBase;
    public float molesdeProtones;
    public float concentraciondeBase;
    public float ccAcido;
    public float mldeAcido = 10;
    public float CantidadProtones;
    public float ConcentracionProtones;
    public float pH2;
    public float ccCargas;
    public float volFinal;
    public float pHFinal;
    public float molesdeBasePorGota;

    public float molesDeAcidoen10ml;

    public int diff = 1;
    // Start is called before the first frame update
    private void Start()
    {
      
        DefaultState();
        //1000ml----0.1mol
        //10ml------0.001mol
        //molesdeProtones = 0.001f;


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NaOHDrop"))
        {
            DropEffect();
            Destroy(other.gameObject);
            drop.Play();
        }

    }

    private void Update()
    {
        pHCalculation();
        Debug.Log("molesdebpg: "+molesdeBasePorGota);
    }

    void pHCalculation()
    {

        if (diff == 1)
        {
            naohml = dropCount / 20;
            pH = -Mathf.Log10(acidCC);
            if (pH >= 7)
            {

                acidSolution.SetActive(false);
                neutralizedSolution.SetActive(true);
                Debug.Log("pH mayor a 7");
            }
            if (pH >= 10)
            {
                neutralizedSolution.SetActive(false);
                basicSolution.SetActive(true);
                Debug.Log("pH mayo a 10");
            }
        }

        { 
       
    }
      
        if (diff == 2)
        {
            ccCargas = 1000 * (molesDeAcidoen10ml - (molesdeBasePorGota * dropCount2)) / (mldeAcido + mldeBase);

            mldeBase = dropCount2 / 20;

            if (ccCargas < 0)
            {
                pH2 = 14 - -Mathf.Log10(-1 * ccCargas);
            }
            if (ccCargas > 0)
            {
                pH2 = -Mathf.Log10(ccCargas);
            }



            if (pH2 >= 7 && pH2 < 10)
            {
                acidSolution.SetActive(false);
                neutralizedSolution.SetActive(true);
                Debug.Log("pH mayor a 7");
            }
            if (pH2 >= 10)
            {
                neutralizedSolution.SetActive(false);
                basicSolution.SetActive(true);
                Debug.Log("pH mayor a 10");
            }
        }
    }
    void DropEffect()
    {
        {
            if (diff == 1)
            {

                dropCount++;
                if ((pH < 3) || (pH > 11))
                {
                    acidCC /= 1.08f;
                }
                if ((pH < 5 && pH > 3) || (pH > 9 && pH < 11))
                {
                    acidCC /= 1.5f;
                }
                if (5 < pH && pH < 9)
                {
                    acidCC /= 9;
                }
            }

            if (diff == 2)
            {
                dropCount2++;
                //molesdeBase += (0.0000005f);
                //molesdeProtones -= (0.0000005f);
            }
        }
    }
    public void DefaultState()
    {
        acidSolution.SetActive(true);
        neutralizedSolution.SetActive(false);
        basicSolution.SetActive(false);
        ConcentracionProtones = 0.1f;
        dropCount = 0;
        dropCount2 = 0;
        acidCC = Random.Range(0.1f, 0.5f);
        initialAcidCC = acidCC;
        protonsCc = acidCC;
        pH = -Mathf.Log10(protonsCc);
    }
    
}
   
