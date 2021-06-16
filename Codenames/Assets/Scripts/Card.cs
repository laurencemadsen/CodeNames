using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour
{
    public enum CardType
    {
        CIVILIAN,
        BLUE,
        RED,
        ASSASSIN
    }

    public Color[] COLORS;
    public Sprite[] cardSprites;
    //public Main _main;
    public Text _cardWord;
    public Button _button;
    public CardType _cardType;
    public Outline _outline;
    bool enabled = true;
    
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {

    }

    void OnDestroy()
    {
    }

    public string getWord()
    {
        return _cardWord.text;
    }

    public void setWord(string word)
    {
        _cardWord.text = word;

    }
    public void setCardType(CardType type)
    {
        _cardType = type;
        _outline.effectColor = COLORS[(int)_cardType];

    }

    public void checkWin()
    {
        int redCount = GameObject.Find("GameBoardPanel").GetComponent<Board>().redCount;
        int blueCount = GameObject.Find("GameBoardPanel").GetComponent<Board>().blueCount;
        Debug.Log(redCount);
        Debug.Log(blueCount);
        if (redCount >= 7)
        {
            endGame("Red");
        }
        else if (blueCount >= 8)
        {
            endGame("Blue");
        }
    }

    public void OnMouseDown()
    {
        if (enabled)
        {
            // Get current team color for reference
            string curTeamStr = GameObject.Find("RedPlayers").GetComponent<turnBehavior>().currentTeam;
            string oppTeam;

            switch (curTeamStr) {
                case "Blue":
                    oppTeam = "Red";
                    break;
                default:
                    oppTeam = "Blue";
                    break;
            }

            // Get redCount and blueCount from Board.cs
            int redCount = GameObject.Find("GameBoardPanel").GetComponent<Board>().redCount;
            int blueCount = GameObject.Find("GameBoardPanel").GetComponent<Board>().blueCount;

            // Get maxCardsForTurn, numCardsClicked from Hint.cs
            int maxCardsForTurn = Int32.Parse(GameObject.Find("MiddlePanel").GetComponent<Hint>().num);
            int numCardsClicked = GameObject.Find("MiddlePanel").GetComponent<Hint>().numCardsClicked;




            // Compare current team to card type
            if (curTeamStr.ToUpper(new System.Globalization.CultureInfo("en-US", false)) == _cardType.ToString()) {
                // If same, add 1 to numCardsClicked, add 1 to teamPoints for current team
                numCardsClicked++;
                if (curTeamStr == "Red")
                    redCount++;
                else
                    blueCount++;
                checkWin();
            } else {
                numCardsClicked = maxCardsForTurn;                  // Not same, turn ends
                if (_cardType == CardType.ASSASSIN) {               // if assassin, end game. oppTeam wins
                    endGame(oppTeam);
                } else if (!(_cardType == CardType.CIVILIAN)) {     // if not civilian, oppTeam gets point
                    if (curTeamStr == "Red")
                        blueCount++;
                    else
                        redCount++;
                    checkWin();
                }
            }

            System.Random rand = new System.Random();
            _button.interactable = false;
            _button.image.sprite = cardSprites[((int)_cardType * 2) + rand.Next(2)];

            // Check if turn over
            bool gameEnded = GameObject.Find("GameBoardPanel").GetComponent<Board>().gameEnded;
            if (numCardsClicked >= maxCardsForTurn && !gameEnded) {
                numCardsClicked = 0;
                GameObject.Find("RedPlayers").GetComponent<turnBehavior>().endTurn();
            }
            GameObject.Find("RedTeamLabel").GetComponent<Text>().text = "Red Team: " + redCount;
            GameObject.Find("BlueTeamLabel").GetComponent<Text>().text = "Blue Team: " + blueCount;

            // Return redCount and blueCount to Board.cs, numCardsClicked to Hint.cs
            GameObject.Find("GameBoardPanel").GetComponent<Board>().redCount = redCount;
            GameObject.Find("GameBoardPanel").GetComponent<Board>().blueCount = blueCount;
            GameObject.Find("MiddlePanel").GetComponent<Hint>().numCardsClicked = numCardsClicked;
        }
    }

    public void setOutlineEnabled()
    {
        _outline.enabled = !_outline.enabled;
    }


    public void disable()
    {
        enabled = false;
    }

    public void enable()
    {
        enabled = true;
    }

    public void endGame(string winningTeam) {
        Debug.Log(winningTeam + " Wins!!");
        Debug.Log(winningTeam + " Wins!!");
        Debug.Log(winningTeam + " Wins!!");
        Debug.Log(winningTeam + " Wins!!");
        Debug.Log(winningTeam + " Wins!!");
        Debug.Log(winningTeam + " Wins!!");
        Debug.Log(winningTeam + " Wins!!");
        Debug.Log(winningTeam + " Wins!!");
        GameObject.Find("GameBoardPanel").GetComponent<Board>().disableCards();
        GameObject.Find("EnterHintButton").GetComponent<Button>().interactable = false;
        GameObject.Find("GameBoardPanel").GetComponent<Board>().gameEnded = true;
        GlobalControl.Instance.winner = winningTeam;
        SceneManager.LoadScene("EndGame");
 
    }
}