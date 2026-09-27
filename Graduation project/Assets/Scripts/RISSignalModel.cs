using UnityEngine;

public class RISSignalModel : MonoBehaviour
{
    public RISController risController;

    public Transform baseStation;
    public Transform userEquipment;
    public float frequencyGHz = 3.5f;

    [Header("RIS Signal Model")]
    public float signalQuality = 0f;

    void Start()
    {
        Debug.Log("RIS Signal Model Started");

        if (risController == null)
        {
            Debug.LogError("RIS Controller is not assigned!");
            return;
        }

        Debug.Log("RIS Controller connected successfully.");
        Debug.Log("Number of RIS Elements: " + risController.elements.Length);

        CalculateRISSignal();
        float CalculateFreeSpacePathLoss(float distance)
        {
            float frequencyHz = frequencyGHz * 1e9f;

            float speedOfLight = 3e8f;

            float wavelength = speedOfLight / frequencyHz;

            float pathLoss =
                20f * Mathf.Log10(
                    (4f * Mathf.PI * distance) / wavelength
                );

            return pathLoss;
        }
    }

    void CalculateRISSignal()
    {
        if (baseStation == null || userEquipment == null)
        {
            Debug.LogError("Base Station or User Equipment is not assigned!");
            return;
        }

        Vector3 bsPosition = baseStation.position;
        Vector3 risPosition = transform.position;
        Vector3 uePosition = userEquipment.position;

        float bsToRISDistance = Vector3.Distance(bsPosition, risPosition);
        float risToUEDistance = Vector3.Distance(risPosition, uePosition);

        Debug.Log("BS → RIS Distance = " + bsToRISDistance + " m");
        Debug.Log("RIS → UE Distance = " + risToUEDistance + " m");

        float bsToRISPathLoss = CalculateFreeSpacePathLoss(bsToRISDistance);
        float risToUEPathLoss = CalculateFreeSpacePathLoss(risToUEDistance);

        Debug.Log("BS → RIS Path Loss = " + bsToRISPathLoss + " dB");
        Debug.Log("RIS → UE Path Loss = " + risToUEPathLoss + " dB");
    }
}