using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadEndGame : MonoBehaviour
{
    public GameObject winnerText;
    // Start is called before the first frame update
    void Start()
    {
        winnerText.GetComponent<Text>().text = GlobalControl.Instance.winner + " Wins!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
