using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErlenmeyerSafe : MonoBehaviour
{
    public Collider bocaDeErlenmeyer;
    public GameObject acidSolution;
    public GameObject neutralizedSolution;
    public GameObject basicSolution;

    public float dropCount;
    public float naohml;
    public float acidCC;
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

    public int diff = 2;

    // Start is called before the first frame update
    private void Start()
    {
        //acidCC = Random.Range(0.1f, 0.5f);
        {
            protonsCc = acidCC;
            pH = -Mathf.Log10(protonsCc);
            protonsCuant = acidCC * 0.001f;
            //protonsCuantity=(acidCC*10)/ 1000;
            //

        }

        //intento 2
        molesdeProtones = 0.001f;
        ConcentracionProtones = (1000 * molesdeProtones) / (mldeAcido + mldeBase);
        //pH2= -Mathf.Log10(ConcentracionProtones);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NaOHDrop"))
        {
            DropEffect(diff);
            { /*
            dropCount++;
            if ((pH<3 )||( pH>11))
            {
                acidCC /= 1.08f;
            }
            if((pH<5&&pH>3)||(pH>9&&pH<11))
            {
                acidCC /= 1.5f;
            }
            if(5<pH && pH<9)
            {
                acidCC /= 9;
            }
            Destroy(other.gameObject);
            */

                //Vv*Cv=Vm*Cm
                //dropCount++;

                //protonsCuantity -= 0.000005f;
                //protonsCc = (protonsCuantity * 1000) / 10;
                //acidCC = (0.1f*naohml) / 10ml;
                //en 1000ml 0.1l en 0.05ml => 0.0000005f


                //intento 2
                /*dropCount2++;
                Destroy(other.gameObject);
                molesdeBase += (0.0000025f / 5);
                molesdeProtones -= (0.0000025f / 5);*/
            }
            Destroy(other.gameObject);


        }
    }

    private void Update()
    {
        pHCalculation(diff);
    }

    public void pHCalculation(int difficulty)
    {
        if (difficulty == 1)
        {

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


        if (difficulty == 2)
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
        }
    }
    void DropEffect(int Difficulty)
    {
        if (Difficulty == 1)
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
        if (Difficulty == 2)
        {
            dropCount2++;

            molesdeBase += (0.0000025f / 5);
            molesdeProtones -= (0.0000025f / 5);
        }
    }
}
