using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OrderTarget : MonoBehaviour
{
    [FormerlySerializedAs("orderInfo")] public TextMeshProUGUI orderTarget;

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
        if (status == process.StatusFail)
        {
            orderTarget.text = "You have prepared the wrong order, please click the cash register to try again";
        }
        // if succeeded
        else if (status == process.StatusSucceed)
        {
            orderTarget.text = "You have prepared the correct order, please click the cash register to continue";
        }
        else if (status == process.StatusInProcess)
        {
            orderTarget.text = "Your target is:\n" + process.FormComboString(process.GetTable());
        }
        else if (status == process.StatusWaitToStart)
        {
            orderTarget.text = "Please click the cash register to start the game";
        }
    }
}