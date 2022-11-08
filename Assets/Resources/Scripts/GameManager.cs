using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    enum GamePhase
    {
        WaitForSignal,
        WaitForShooting,
        WaitForReset
    }

    #region Timer
    float countDownTime;
    float countDownTimer;
    bool timerActive;
    Vector2 timeRange = new Vector2(3,7);


    private void StartTimer()
    {
        countDownTimer = countDownTime;
        timerActive = true;
    }

    private void UpdateTimer()
    {
        if (timerActive)
        {
            countDownTimer -= Time.deltaTime;
            if (countDownTimer <= 0)
            {
                InvokeTimer();
                EndTimer();
            }
        }
    }

    private void InvokeTimer()
    {
        //SphereSignal.material = Resources.Load<Material>("Materials/RedMat");

    }

    private void EndTimer()
    {
        timerActive = false;
    }

    #endregion

    public Cowboy cowboy1;
    public Cowboy cowboy2;
    public Signal signal;

    GamePhase gamePhases;

    private char keyPressed;

    // Start is called before the first frame update
    private void Start()
    {
        ResetRound();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckKeyPressed();
        
        switch (gamePhases)
        {
            case GamePhase.WaitForSignal:
                WaitForSignalUpdate();
                break;
            case GamePhase.WaitForShooting:
                WaitForShootingUpdate();
                break;
            case GamePhase.WaitForReset:
                WaitForResetUpdate();
                break;
            default:
                Debug.Log("unhandeled switch : " + gamePhases);
                break;
        }
    }

    private void WaitForSignalUpdate()
    {
        if(Time.time >= countDownTime)
        {
            SignalFires();
        }
    }

    private void WaitForShootingUpdate()
    {
        switch (keyPressed)
        {
            case 'a':
                cowboy1.CowboyShoots();
                cowboy2.CowboyDies(cowboy1.direction);
                gamePhases = GamePhase.WaitForReset;
                break;
            case 'l':
                cowboy2.CowboyShoots();
                cowboy1.CowboyDies(cowboy2.direction);
                gamePhases = GamePhase.WaitForReset;
                break;
            default:
                break;
        }
    }

    private void WaitForResetUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetRound();
        }
    }

    private void ResetRound()
    {
        //Reset Cowboy positions
        //reset signal color
        //Start main timer

        cowboy1.ResetCowboy();
        cowboy2.ResetCowboy();
        signal.SetSignalRed();

        countDownTime = Time.time + Random.Range(timeRange.x, timeRange.y);
        gamePhases = GamePhase.WaitForSignal;

        keyPressed = ' ';
    }

    private void SignalFires()
    {
        //Change signal color to green
        //Play sound effect
        //enter ready to shoot phase

        signal.SetColorGreen();
        signal.PlayBellSound();
        gamePhases = GamePhase.WaitForShooting;
    }

    private void CheckKeyPressed()
    {
        //Depends on the phase
        //if WaitForSignal phase, cowboy looses
        //if WaitforShooting phase, cowboy wins
        //if WaitForReset phase, nothing happens
        if (Input.anyKeyDown)
        {
            switch (Input.inputString.Substring(0,1))
            {
                case "a":
                    Debug.Log("key:a");
                    if(gamePhases != GamePhase.WaitForShooting)
                    {
                        cowboy1.CowboyDies(cowboy2.direction);
                        gamePhases = GamePhase.WaitForReset;
                    }
                    else
                    {
                        keyPressed = 'a';
                    }
                    break;
                case "l":
                    Debug.Log("key:l");
                    if (gamePhases != GamePhase.WaitForShooting)
                    {
                        cowboy2.CowboyDies(cowboy1.direction);
                        gamePhases = GamePhase.WaitForReset;
                    }
                    else
                    {
                        keyPressed = 'l';
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
