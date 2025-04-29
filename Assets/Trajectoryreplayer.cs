using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;


public class TrajectoryReplayer : MonoBehaviour
{
    private List<Vector3> positions = new List<Vector3>();
    private int currentIndex = 0;
    public float moveSpeed = 5f; // Bewegungsgeschwindigkeit
    private bool isReplaying = false;

    void Start()
    {
        LoadTrajectory();
    }

    void Update()
    {
        if (isReplaying && positions.Count > 0)
        {
            MoveAlongTrajectory();
        }
    }

    private void LoadTrajectory()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "trajectory.csv");

        if (!File.Exists(filePath))
        {
            Debug.LogError("Trajectory file not found: " + filePath);
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++) // Start bei 1, um Header zu überspringen
        {
            string[] parts = lines[i].Split(',');

            if (parts.Length >= 4)
            {
                float x = float.Parse(parts[1], CultureInfo.InvariantCulture);
                float y = float.Parse(parts[2], CultureInfo.InvariantCulture);
                float z = float.Parse(parts[3], CultureInfo.InvariantCulture);

                positions.Add(new Vector3(x, y, z));
            }
        }

        if (positions.Count > 0)
        {
            isReplaying = true;
            transform.position = positions[0]; // Start an erster gespeicherter Position
            Debug.Log("✅ Trajectory loaded, steps: " + positions.Count);
        }
    }

    private void MoveAlongTrajectory()
    {
        if (currentIndex >= positions.Count)
            return;

        Vector3 targetPosition = positions[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)

            Debug.Log("Aktuelle Position: " + transform.position + " → Ziel: " + targetPosition);
        {
            currentIndex++;
        }
    }
}