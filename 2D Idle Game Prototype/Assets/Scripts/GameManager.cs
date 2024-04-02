using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float points;
    public TextMeshProUGUI scoreTxt;
    public float earnAmt = 1;
    public float multi = 1;
    public float costMulti = 25;
    public TextMeshProUGUI multiTxt;
    public TextMeshProUGUI multiButtonTxt;
    public ParticleSystem part;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //do passive generation upgrade
    }

    public void Clicked()
    {
        points+= earnAmt * multi;
        scoreTxt.text = "LUDDIES: " + Mathf.CeilToInt(points) + "L";
        part.Play();
    }

    public void MultiplierUpgrade()
    {
        if(points >= costMulti)
        {
            points -= costMulti;
            multi += 0.05f;
            costMulti *= multi + .1f;
            multiTxt.text = "Multiplier: " + multi + "x";
            multiButtonTxt.text = "Upgrade Multiplier\n COST: " + Mathf.RoundToInt(costMulti) + "L";
        }
    }
}
