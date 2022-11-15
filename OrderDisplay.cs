using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OrderDisplay : MonoBehaviour
{
    public TextMeshProUGUI orderInfo;

    private string _orderMission;

    private string _orderPreparation;

    private float _deltaTime;

    // Start is called before the first frame update
    void Start()
    {
        _orderMission = "Your mission is:";
        _orderPreparation = "Your preparation is:";
        orderInfo.text = _orderMission + "\n" + _orderPreparation;
    }

    void Update()
    {
        _deltaTime += Time.deltaTime;
        // update every 0.5 seconds
        if (_deltaTime >= 0.5f)
        {
            Process process = CameraPointer.process;
            _orderMission = "Your mission is:" + process.FormComboString(process.GetTable());
            _orderPreparation = "Your preparation is:" + process.FormComboString(process.GetPlate());
            orderInfo.text = _orderMission + "\n" + "\n" + _orderPreparation;
            int status = process.GetStatus();
            // if failed
            if (status == -1)
            {
                orderInfo.text = _orderMission + "\n" + "\n" + "You have prepared the wrong order, please try again";
                process.SetStatus(0);
                // if succeeded
            }
            else if (status == 1)
            {
                orderInfo.text = "You have prepared the correct order, please continue";
                process.SetStatus(0);
            }
            _deltaTime = 0;
        }
    }
}