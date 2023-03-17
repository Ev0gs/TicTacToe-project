using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] spaceList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;

    public GameObject winLine;
    public RectTransform winLineRectTransform;

    public CanvasGroup fade;

    private string side;
    private int moves;


    // Start is called before the first frame update
    void Start()
    {
        SetGameControllerReferenceForButtons();

        // Starting game fade
        LeanTween.alphaCanvas(fade, 0, 2.5f);
        
        // Game initialization
        side = "X";
        gameOverPanel.SetActive(false);
        moves = 0;
        restartButton.SetActive(false);
    }

    void SetGameControllerReferenceForButtons()
    {
        for (int i = 0; i < spaceList.Length; i++)
        {
            spaceList[i].GetComponentInParent<Space>().SetControllerReference(this);
        }
    }

    public string GetSide()
    {
        return side;
    }

    void ChangeSide()
    {
        if(side == "X")
        {
            side = "O";
        }
        else
        {
            side = "X";
        }
    }

    public void EndTurn()
    {
        moves++;

        // HORIZONTAL TOP LINE
        if(spaceList[0].text == side && spaceList[1].text == side && spaceList[2].text == side)
        {
            winLineRectTransform.anchoredPosition = new Vector2(0, 100);
            StartCoroutine(GameOver());
            /*GameOver();*/

        }
        // HORIZONTAL MIDDLE LINE
        else if (spaceList[3].text == side && spaceList[4].text == side && spaceList[5].text == side)
        {
            winLineRectTransform.anchoredPosition = new Vector2(0, 0);
            StartCoroutine(GameOver());
            /*GameOver();*/
        }
        // HORIZONTAL BOTTOM LINE
        else if(spaceList[6].text == side && spaceList[7].text == side && spaceList[8].text == side)
        {
            winLineRectTransform.anchoredPosition = new Vector2(0, -100);
            StartCoroutine(GameOver());
        }
        // VERTICAL LEFT LINE
        else if(spaceList[0].text == side && spaceList[3].text == side && spaceList[6].text == side)
        {
            winLineRectTransform.Rotate(new Vector3(0, 0, 90));
            winLineRectTransform.anchoredPosition = new Vector2(-105, 0);
            StartCoroutine(GameOver());
        }
        // VERTICAL MIDDLE LINE
        else if(spaceList[1].text == side && spaceList[4].text == side && spaceList[7].text == side)
        {
            winLineRectTransform.Rotate(new Vector3(0, 0, 90));
            winLineRectTransform.anchoredPosition = new Vector2(0, 0);
            StartCoroutine(GameOver());
        }
        // VERTICAL RIGHT LINE
        else if(spaceList[2].text == side && spaceList[5].text == side && spaceList[8].text == side)
        {
            winLineRectTransform.Rotate(new Vector3(0, 0, 90));
            winLineRectTransform.anchoredPosition = new Vector2(105, 0);
            StartCoroutine(GameOver());
        }
        // DIAG LEFT TOP TO BOTTOM RIGHT 
        else if(spaceList[0].text == side && spaceList[4].text == side && spaceList[8].text == side)
        {
            winLineRectTransform.Rotate(new Vector3(0, 0, -45));
            StartCoroutine(GameOver());
        }
        // DIAG RIGHT  TOP TO BOTTOM LEFT 
        else if(spaceList[2].text == side && spaceList[4].text == side && spaceList[6].text == side)
        {
            winLineRectTransform.Rotate(new Vector3(0, 0, 45));
            StartCoroutine(GameOver());
        }
        else if (moves >= 9)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "Tie";
            restartButton.SetActive(true);
        }
        else
        {
            ChangeSide();
        }
    }

    IEnumerator GameOver()
    {
        LeanTween.scaleX(winLine, 1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        gameOverPanel.SetActive(true);
        gameOverText.text = side + " side wins !";
        restartButton.SetActive(true);
        for (int i = 0; i < spaceList.Length; i++)
        {
            SetInteractable(false);
        }
    }
    /*void GameOver()
    {
        LeanTween.scaleX(winLine, 1, 0.5f);
        StartCoroutine(wait());
        gameOverPanel.SetActive(true);
        gameOverText.text = side + "wins !";
        restartButton.SetActive(true);
        for (int i = 0; i < spaceList.Length; i++)
        {
            SetInteractable(false);
        }
    }*/

    void SetInteractable(bool setting)
    {
        for (int i = 0; i < spaceList.Length; i++)
        {
            spaceList[i].GetComponentInParent<Button>().interactable = setting;
        }
    }

    public void Restart()
    {
        side = "X";
        moves = 0;
        winLineRectTransform.anchoredPosition = new Vector2(0, 0);
        winLineRectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        winLineRectTransform.localScale = new Vector3(0, 1, 0);
        gameOverPanel.SetActive(false);
        SetInteractable(true);
        restartButton.SetActive(false);
        for (int i = 0; i < spaceList.Length; i++)
        {
            spaceList[i].text = " ";
        }
    }
}
