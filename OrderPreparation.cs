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
        }
    }
}