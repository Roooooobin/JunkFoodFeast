using TMPro;
using UnityEngine;

public class OrderTarget : MonoBehaviour
{
    public TextMeshProUGUI orderTarget;

    // Start is called before the first frame update
    void Start()
    {
        orderTarget.text = "Your mission is:";
    }

    void Update()
    {
        Process process = CameraPointer.process;
        int status = process.GetStatus();
        // if failed
        if (status == process.statusFail)
        {
            orderTarget.text = "You have prepared the wrong order, please click the cash register to try again";
        }
        // if succeeded
        else if (status == process.statusSucceed)
        {
            orderTarget.text = "You have prepared the correct order!";
            process.intervalBetweenTwoTasks += Time.deltaTime;
            // after 1s, automatically assign a new task
            if (process.intervalBetweenTwoTasks >= 1)
            {
                process.SetStatus(process.statusInProcess);
                // set interval to 0 again
                process.intervalBetweenTwoTasks = 0;
            }
        }
        else if (status == process.statusInProcess)
        {
            orderTarget.text = "Your target is:\n" + process.FormComboString(process.GetTable());
        }
        else if (status == process.statusWaitToStart)
        {
            orderTarget.text = "Please click the cash register to start the game";
        }
        else if (status == process.statusEndGame)
        {
            orderTarget.text = "Congrats, you got " + process.GetScore() + " point(s)\n";
            orderTarget.text += "This round of the game is ended, click the cash register to start a new one";
        }

        // show countdown left if the game is not ended
        if (status != process.statusEndGame)
        {
            orderTarget.text += "\n" + "\n";
            orderTarget.text += "Countdown: " + process.GetCountdown() + "s";
        }
    }
}