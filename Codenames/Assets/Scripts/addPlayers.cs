using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class addPlayers : MonoBehaviour
{   
    // variables 
    public GameObject inputField;       // the text box
    public GameObject displayText;      // the text field
    public GameObject errorText;        // text if names is taken
    public GameObject minPlayerText;        // text if names is taken
    public GameObject inputText;        // to clear the text from text field
    public const int MAX_PLAYERS = 8;   // max players allowed in a game
    public createPlayer[] players = new createPlayer[MAX_PLAYERS];    // create array of 6 players
    bool assigned = false;              // alternate value to alternate between teams
    int numBlue = 0;                    // Number of players on the Blue team
    int numRed = 0;                     // Number of players on the Red team
    public int spyRed = 0;
    public int spyBlue = 0;
    public string spymasterBanner;

    // class to create a player
    public class createPlayer {

        // variables
        public string playerName;
        public bool isSpymaster;
        public string teamColor;

        // deafult const
        public createPlayer() {
            playerName = "";
            isSpymaster = false;
            teamColor = "";
        }

        // display name to console
        public string displayName() {
            return playerName;
        }

        // get text and set text from user
        public void getSetInfo(GameObject input, GameObject output) {
            // get player name from text box
            playerName = input.GetComponent<Text>().text;
            // add name to displayed text field
            output.GetComponent<Text>().text = output.GetComponent<Text>().text + playerName + "\n";
        }

        // set the player team
        public void setTeam(string team) {
            teamColor = team;
        }

        public string getTeam() {
            return teamColor;
        }

        // set if spymaster or not
        public void setSpy(bool isSpy) {
            isSpymaster = isSpy;
        }

    }   

    // add a player to game
    public void getName() {
        // create player object
        createPlayer player = new createPlayer();
        errorText.GetComponent<Text>().text = "";
        // keep track if name is taken
        bool taken = false;

        // check array of names
        for (int i = 0; i < players.Length; i++) {
            // if a player exists
            if (players[i] != null) {
                // find the name the player entered
                name = inputField.GetComponent<Text>().text;
                if (players[i].playerName == name) {
                    // name is taken
                    taken = true;
                    errorText.GetComponent<Text>().text = "'" + name + "' is taken!";
                    inputText.GetComponent<InputField>().text = "";
                    break;
                } 
                if (name == " ") {
                    // changed taken to true so it wont add space as a name
                    taken = true;
                    errorText.GetComponent<Text>().text = "Please enter a valid name!";
                    inputText.GetComponent<InputField>().text = "";
                    break;
                }
                if (name.Length > 13) {
                    taken = true;
                    errorText.GetComponent<Text>().text = "Name is too long!!";
                    inputText.GetComponent<InputField>().text = "";
                    break;
                }
            }
        }

        // if name isn't taken
        if (taken != true) {
            // find empty spot in array
            for (int i = 0; i < players.Length; i++) {
                if (players[i] == null) {
                    // add player to game
                    players[i] = player;
                    players[i].getSetInfo(inputField, displayText);
                    inputText.GetComponent<InputField>().text = "";
                    break;
                }
            }
        }
    }

    // assign players to teams
    public void makeTeams() {
        // check if enough players 
        if (players[3] == null) {
            minPlayerText.GetComponent<Text>().text = "Not enough players!";
            return;
        }
        // iterate through each player, assigning a team
        for (int i = 0; i < players.Length; i++) {
            if (players[i] == null) {
                break;
            }   
            if (assigned == false) {
                players[i].setTeam("Red");
                assigned = true;
            } else {
                players[i].setTeam("Blue");
                assigned = false;
            }
        }
        pickSpymasters();
    }

    public void pickSpymasters() {
        System.Random rand = new System.Random();
        
        int randRed = rand.Next(1, numRed + 1);
        int randBlue = rand.Next(1, numBlue + 1);
        int curRed = 0, curBlue = 0;

        for (int i = 0; i < players.Length; i++) {
            if (players[i] == null)
            {
                break;
            }
            if (players[i].getTeam() == "Red") {
                curRed++;
                if (curRed == randRed) {
                    players[i].setSpy(true);
                    spyRed = i;
                    continue;
                }
            }
            if (players[i].getTeam() == "Blue") {
                curBlue++;
                if (curBlue == randBlue) {
                    players[i].setSpy(true);
                    spyBlue = i;
                    continue;
                }
            }
        }

        GlobalControl.Instance.players = players;
        SceneManager.LoadScene("GameScene");
    }
}


