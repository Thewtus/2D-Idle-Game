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
    public ParticleSystem[] part;
    public float ptsMiner = 1;
    public float mineUpgrades = 0;
    public TextMeshProUGUI ptsMinerTxt;
    public TextMeshProUGUI minerButtonTxt;
    public GameObject minerImage;
    public float costMiner = 15;

    public TextMeshProUGUI objTxt;
    public TextMeshProUGUI objButtonTxt;
    public float objTimer = 300;
    public float costObj = 200;
    bool isGameOver = false;
    public TextMeshProUGUI GOTxt;
    private static float MAX_TIMER;

    // Start is called before the first frame update
    void Start()
    {
        MAX_TIMER = objTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(objTimer > 0)
        {
            objTimer -= Time.deltaTime;
            objTxt.text = Mathf.RoundToInt(objTimer).ToString();
        }
        else
        {
            gameOver();
        }
        //do passive generation upgrade
        if(ptsMiner > 0 && mineUpgrades > 0)
        {
            ptsMiner -= Time.deltaTime;
        }
        else if (ptsMiner <= 0 && !isGameOver) 
        {
            points += 1;
            SpriteRenderer sr = minerImage.GetComponent<SpriteRenderer>();
            sr.flipX = !sr.flipX;
            part[1].Play();
            ptsMiner = 1 / mineUpgrades;
        }
        scoreTxt.text = "LUDDIES: " + Mathf.CeilToInt(points) + "L";
    }

    public void Clicked()
    {
        points+= earnAmt * multi;
        part[0].Play();
    }

    public void MultiplierUpgrade()
    {
        if(points >= costMulti)
        {
            points -= costMulti;
            multi += 0.1f;
            costMulti *= multi + .15f;
            multiTxt.text = "ROBBIE MULTIPLIER: " + multi + "x";
            multiButtonTxt.text = "Upgrade Multiplier\n COST: " + Mathf.RoundToInt(costMulti) + "L";
        }
    }

    public void MinerUpgrade()
    {
        if( points >= costMiner)
        {
            points -= costMiner;
            costMiner *= 2 + (.05f * mineUpgrades);
            if (mineUpgrades == 0)
            {
                minerImage.gameObject.SetActive(true);
                ptsMinerTxt.gameObject.SetActive(true);
            }
            mineUpgrades++;
            ptsMinerTxt.text = "Auto Miner: " + mineUpgrades + "L/s";
            minerButtonTxt.text = "Upgrade Multiplier\n COST: " + Mathf.RoundToInt(costMiner) + "L";
        }
        

    }

    private void gameOver()
    {
        isGameOver = true;
        GOTxt.gameObject.SetActive(true);
        multiButtonTxt.GetComponentInParent<GameObject>().SetActive(false);
        minerButtonTxt.GetComponentInParent<GameObject>().SetActive(false);
    }

    public void ExtendObjective()
    {
        if(points >= costObj)
        {
            points -= costObj;
            objTimer = MAX_TIMER;
            costObj *= 2.5f;
            objButtonTxt.text = "EXTEND ASSIGNMENT\nCOST: " + Mathf.RoundToInt(costObj) + "L";
        }
    }
}
