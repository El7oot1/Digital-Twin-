using UnityEngine;

public class RISController : MonoBehaviour
{
    public Transform[] elements;

    public int rows = 4;
    public int columns = 4;

    // Phase of each RIS element
    public float[] phases = new float[16];


    public bool useManualConfiguration = true;

    void Start()
    {
        Debug.Log("RIS Controller Started");
        Debug.Log("Number of RIS Elements: " + elements.Length);

        if (elements.Length != rows * columns)
        {
            Debug.LogError("RIS must have exactly " + (rows * columns) + " elements");
            return;
        }

        // Create phase array only if it does not exist
        if (phases == null || phases.Length != elements.Length)
        {
            phases = new float[elements.Length];
        }

        Debug.Log("RIS Phase System Ready");

        if (!useManualConfiguration)
        {
            float[] testConfiguration =
            {
        0f, 30f, 60f, 90f,
        30f, 60f, 90f, 120f,
        60f, 90f, 120f, 150f,
        90f, 120f, 150f, 180f
    };

            SetAllPhases(testConfiguration);
        }
        else
        {
            // Apply manual phases from Inspector
            SetAllPhases(phases);
        }
    }

    // Set phase for one RIS element
    public void SetPhase(int index, float phase)
    {
        if (index < 0 || index >= elements.Length)
        {
            Debug.LogError("Invalid RIS element index");
            return;
        }

        // Keep phase between 0 and 360
        phase = phase % 360f;

        if (phase < 0)
            phase += 360f;

        phases[index] = phase;

        // Visual representation of phase
        elements[index].localRotation = Quaternion.Euler(0f, 0f, phase);

        Debug.Log("Element " + (index + 1) + " Phase = " + phase + "°");
    }

    // Set phase configuration for all RIS elements
    public void SetAllPhases(float[] newPhases)
    {
        if (newPhases.Length != elements.Length)
        {
            Debug.LogError(
                "Phase configuration must contain exactly " +
                elements.Length + " values."
            );
            return;
        }

        for (int i = 0; i < elements.Length; i++)
        {
            SetPhase(i, newPhases[i]);
        }

        Debug.Log("RIS configuration updated successfully.");
    }
}