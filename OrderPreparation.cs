using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class OrderPreparation : MonoBehaviour
{
    [FormerlySerializedAs("orderInfo")] public TextMeshProUGUI orderPreparation;
    
    private string _orderPreparation;
    
    // Start is called before the first frame update
    void Start()
    {
        orderPreparation.text = "Your preparation is:";
    }

    void Update()
    {
        Process process = CameraPointer.process;
        orderPreparation.text = "Your preparation is:" + process.FormComboString(process.GetPlate());
    }
}