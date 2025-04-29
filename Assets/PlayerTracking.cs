using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public Transform player;
    public float trackingInterval = 0.1f; // in Sekunden
    private float timer = 0f;

    private List<string> dataLines = new List<string>();

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        dataLines.Add("time,x,y,z"); // CSV-Header
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= trackingInterval)
        {
            string line = Time.time.ToString("F2") + "," +
                          player.position.x.ToString("F4") + "," +
                          player.position.y.ToString("F4") + "," +
                          player.position.z.ToString("F4");
            dataLines.Add(line);
            timer = 0f;
        }
    }

    void OnApplicationQuit()
    {
        string path = Application.dataPath + "/trajectory.csv";
        File.WriteAllLines(path, dataLines.ToArray());
        Debug.Log("Trajectory saved to: " + path);
    }
}
