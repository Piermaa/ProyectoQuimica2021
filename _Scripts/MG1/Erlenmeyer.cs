using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erlenmeyer : MonoBehaviour
{
    public Collider bocaDeErlenmeyer;
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
    public float mldeAcido =10;
    public float CantidadProtones;
    public float ConcentracionProtones;
    public float pH2;
    public float ccCargas;

    public int diff=1;
    // Start is called before the first frame update
    private void Start()
    {
        acidCC = Random.Range(0.1f, 0.5f);
        initialAcidCC = acidCC;
        protonsCc = acidCC;
        pH = -Mathf.Log10(protonsCc);
        DefaultState();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NaOHDrop"))
        {
            DropEffect();
            Destroy(other.gameObject);
        }
    
    }

    private void Update()
    {
        pHCalculation();
       
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

        if (diff == 2)
        {
            mldeBase = dropCount2 * 0.05f;

            ConcentracionProtones = 1000 * (0.001f - molesdeBase) / (10f + mldeBase);
            if (mldeBase > 0)
            {
                concentraciondeBase = 1000 * (molesdeBase - molesdeProtones) / (mldeAcido + mldeBase);
            }
            ccCargas = ConcentracionProtones - concentraciondeBase;
            if (ccCargas > 0)
            {

                pH2 = -Mathf.Log10(ccCargas);
            }
            if (ccCargas < 0)
            {
                pH2 = 14 - -Mathf.Log10(-1 * ccCargas);
            }
            if (ccCargas == 0)
            {
                pH2 = 7;
            }

            if (pH2 >= 7)
            {

                acidSolution.SetActive(false);
                neutralizedSolution.SetActive(true);
                Debug.Log("pH mayor a 7");
            }
            if (pH2 >= 10)
            {
                neutralizedSolution.SetActive(false);
                basicSolution.SetActive(true);
                Debug.Log("pH mayo a 10");
            }
            //TODO: Si tiran 30 gotas explota todo
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
                molesdeBase += (0.0000025f / 5);
                molesdeProtones -= (0.0000025f / 5);
            }


        }
    }
    public void DefaultState()
    {
        acidSolution.SetActive(true);
        neutralizedSolution.SetActive(false);
        basicSolution.SetActive(false);
        dropCount = 0;
        dropCount2 = 0;
    }
}
