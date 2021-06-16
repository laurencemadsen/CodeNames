using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnBehavior : MonoBehaviour
{
    public string currentTeam = "Blue";

    public void setCurrentTeam(string team) {
        currentTeam = team;

        // Set team member names to bold to denote the current team
        GameObject blueTeamNames = GameObject.Find("BluePlayers");
        GameObject redTeamNames = GameObject.Find("RedPlayers");
        if (currentTeam == "Red") {
            redTeamNames.GetComponent<Text>().color = Color.red;
            blueTeamNames.GetComponent<Text>().color = Color.black;
        }
        else if (currentTeam == "Blue") {
            blueTeamNames.GetComponent<Text>().color = Color.blue;
            redTeamNames.GetComponent<Text>().color = Color.black;
        }
    }

    public void endTurn() {
        if (currentTeam == "Blue")
            setCurrentTeam("Red");
        else
            setCurrentTeam("Blue");

        // Disable cards, enable hint button for spymaster
        GameObject.Find("HintDisplayText").GetComponent<Text>().text = "Waiting for " +currentTeam+ " Spymaster...";
        GameObject.Find("GameBoardPanel").GetComponent<Board>().disableCards();
        GameObject.Find("EnterHintButton").GetComponent<Button>().interactable = true;
    }
}