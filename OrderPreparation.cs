using TMPro;
using UnityEngine;

public class OrderPreparation : MonoBehaviour
{
    public TextMeshProUGUI orderPreparation;

    // Start is called before the first frame update
    void Start()
    {
        orderPreparation.text = "Your preparation is:";
    }

    void Update()
    {
        Process process = CameraPointer.process;
        if (process != null)
        {
            orderPreparation.text = "Your preparation is:" + process.FormComboString(process.GetPlate());
            if (process.GetStatus() == process.statusEndGame)
            {
                orderPreparation.text = "Congrats, you got " + process.GetScore() + " point(s)\n";
                orderPreparation.text += "This round of the game is ended, click the cash register to start a new one";
            }
        }
    }
}