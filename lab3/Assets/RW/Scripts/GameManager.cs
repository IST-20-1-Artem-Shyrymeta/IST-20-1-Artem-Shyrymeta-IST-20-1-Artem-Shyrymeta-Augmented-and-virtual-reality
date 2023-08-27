/*
 * Copyright (c) 2018 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public struct Order
{
    public bool pepperoni;
    public bool pineapple;
    public bool mushroom;
}

public class GameManager : MonoBehaviour
{
    //scene references
    public GameObject pizza;
    public GameObject pepperoni;
    public GameObject mushroom;
    public GameObject pineapple;

    public Text timer;

    public static GameManager instance;

    public static float timeRemaining;
    private static int roundScore;
    private static Dictionary<int, int> accumulatedScore = new Dictionary<int, int>(5);

    public Order currentOrder;
    public UnityEvent onNewOrderCreated;
    public IntEvent onOrderComplete;

    public IntEvent onGameComplete;

    public UnityEvent onGameRestart;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            RestartGame();
        }
        else
        {
            Destroy(this);
        }
    }

    private void Init()
    {
        roundScore = 3;
        timeRemaining = 60;
        accumulatedScore.Clear(); //clear any previous scores
        accumulatedScore.Add(1, 0); //1 star scores
        accumulatedScore.Add(2, 0); //2 star scores
        accumulatedScore.Add(3, 0); //3 star scores
        accumulatedScore.Add(4, 0); //4 star scores
        accumulatedScore.Add(5, 0); //5 star scores

        CreateNewOrder();
    }

    //check if user's order matches current order
    public void CompleteOrder()
    {
        //judge the completed pizza
        roundScore = 2;
        roundScore = mushroom.activeInHierarchy == currentOrder.mushroom ?  roundScore + 1 : roundScore - 1;
        roundScore = pepperoni.activeInHierarchy == currentOrder.pepperoni ?  roundScore + 1 : roundScore - 1;
        roundScore = pineapple.activeInHierarchy == currentOrder.pineapple ?  roundScore + 1 : roundScore - 1;

        //clamp score between 1 and 5 stars
        roundScore = Mathf.Clamp(roundScore, 1, 5);
        onOrderComplete.Invoke(roundScore);
        Debug.Log(roundScore);
        //add this pizza to the total score
        Debug.Log(accumulatedScore.Keys.Count + " " + accumulatedScore.Values.Count);
        accumulatedScore[roundScore]++;

        //can we make another pizza?
        if (timeRemaining > 0)
        {
            CreateNewOrder();
        }
        else
        {
            //Finish the game
            GameComplete();
        }
    }

    //create a new order
    public void CreateNewOrder()
    {
        //clear the old settings
        mushroom.SetActive(false);
        pineapple.SetActive(false);
        pepperoni.SetActive(false);

        //create new order with random request
        currentOrder = new Order();
        currentOrder.mushroom = Random.value > 0.5f;
        currentOrder.pepperoni = Random.value > 0.5f;
        currentOrder.pineapple = Random.value > 0.5f;
        
        onNewOrderCreated.Invoke();
    }

    public void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if(timer != null)
            {
                timer.text = Mathf.RoundToInt(timeRemaining).ToString();
            }
        }
    }

    private void GameComplete()
    {
        int totalScore = 0;

        totalScore += accumulatedScore[5] * 5;
        totalScore += accumulatedScore[4] * 4;
        totalScore += accumulatedScore[3] * 3;
        totalScore += accumulatedScore[2] * 2;
        totalScore += accumulatedScore[1] * 1;

        onGameComplete.Invoke(totalScore);
    }

    public void RestartGame()
    {
        onGameRestart.Invoke();
        Init();
    }
}
