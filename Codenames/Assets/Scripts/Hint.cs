using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    public string hint;
    public string num;
    public GameObject inputField;
    public GameObject numberInputField;
    public GameObject display;
    public int numCardsClicked = 0;

    public void storeHint(){
        hint = inputField.GetComponent<Text>().text;
        num = numberInputField.GetComponent<Text>().text;
        display.GetComponent<Text>().text = GameObject.Find("RedPlayers").GetComponent<turnBehavior>().currentTeam + "'s Turn\n";
        display.GetComponent<Text>().text += "Hint: " + hint +", "+num;

        // Enable the cards for operative use
        GameObject.Find("GameBoardPanel").GetComponent<Board>().enableCards();

        // Disable hint button until operative turn over
        GameObject.Find("EnterHintButton").GetComponent<Button>().interactable = false;
    }

    public string getHint() {
        return hint;
    }
}
