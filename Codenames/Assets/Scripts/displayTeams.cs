using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static addPlayers;

public class displayTeams : MonoBehaviour
{
    public GameObject redText;          // text box to display red team
    public GameObject blueText;          // text box to display red team
    public const int MAX_PLAYERS = 8;   // max players allowed in a game
    public createPlayer[] players = new createPlayer[MAX_PLAYERS];    // create array of 6 players

    // Start is called before the first frame update
    void Start()
    {   
        players = GlobalControl.Instance.players;

        for (int i = 0; i < players.Length; i++) {
            if (players[i] == null) {
                break;
            }   
            if (players[i].teamColor == "Red") {
                redText.GetComponent<Text>().text = 
                redText.GetComponent<Text>().text + "\n" + players[i].playerName;
            } else {
                blueText.GetComponent<Text>().text = 
                blueText.GetComponent<Text>().text +  "\n" + players[i].playerName;
            }
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
