using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class TrajectoryLogger : MonoBehaviour
{
    private List<string> trajectoryData = new List<string>();
    private string filePath;

    void Start()
    {
        // Speichere im Assets-Ordner (du kannst den Pfad ändern)
        filePath = Path.Combine(Application.persistentDataPath, "trajectory.csv");

        // Schreibe die Header-Zeile
        File.WriteAllText(filePath, "time,x,y,z\n");
    }

    void Update()
    {
        // Aktuelle Position und Zeit erfassen
        Vector3 position = transform.position;
        float currentTime = Time.time;

        // Format: Komma als Trennzeichen, Punkt als Dezimalzeichen
        string line = string.Format(CultureInfo.InvariantCulture,
            "{0:F2},{1:F4},{2:F4},{3:F4}",
            currentTime, position.x, position.y, position.z);

        trajectoryData.Add(line);
    }

    void OnApplicationQuit()
    {
        // Alle gesammelten Daten am Ende speichern
        File.AppendAllLines(filePath, trajectoryData);
        Debug.Log("✅ Trajektorie gespeichert: " + filePath);
    }
}
