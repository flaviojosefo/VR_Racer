using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Race : MonoBehaviour {

    [SerializeField] private Transform pointsParent;
    [SerializeField] private Text timeText;
    [SerializeField] private TextMesh bestText; 

    private List<Transform> wayPoints;

    private int currentWayPoint = -1;
    private float currentTime = 0.0f;
    private float bestTime = float.MaxValue;

    private string wayPointName;
    private string nextName;

    void Start() {

        GetWaypoints();
        UpdateCurrentWayPoint(currentWayPoint);
    }

    private void GetWaypoints() {

        wayPoints = new List<Transform>();

        foreach (Transform t in pointsParent) {

            wayPoints.Add(t);
        }
    }

    private void UpdateCurrentWayPoint(int current) {

        for (int i = 0; i < wayPoints.Count; i++) {

            if (i == current) {

                UpdateWaypointMaterials(wayPoints[i], Color.white);

            } else if (i == current + 1) {

                nextName = wayPoints[i].name;
                UpdateWaypointMaterials(wayPoints[i], Color.red);
            }
        }

        if (current == 0) {

            StartCoroutine(UpdateTimer());

        } else if (current == wayPoints.Count - 1) {

            UpdateBestTime();

            currentTime = 0;

            currentWayPoint = -1;
            UpdateCurrentWayPoint(currentWayPoint);

            StopAllCoroutines();

            return;
        }

        currentWayPoint++;
    }

    private void UpdateWaypointMaterials(Transform wayPoint, Color color) {

        foreach (Transform t in wayPoint) {

            t.GetComponent<Renderer>().material.color = color;
        }
    }

    private IEnumerator UpdateTimer() {

        while (currentWayPoint != wayPoints.Count) {

            currentTime += Time.deltaTime;

            TimeSpan timespan = TimeSpan.FromSeconds(currentTime);
            timeText.text = timespan.ToString(@"mm\:ss");

            yield return null;
        }
    }

    private void UpdateBestTime() {

        if (bestTime > currentTime) {

            bestTime = currentTime;
        }

        TimeSpan timespan = TimeSpan.FromSeconds(bestTime);
        bestText.text = "Best Time: " + timespan.ToString(@"mm\:ss");
    }

    private void OnTriggerEnter(Collider other) {
        
        if (other.CompareTag("Waypoint")) {

            if (wayPointName == other.name) return;
            wayPointName = other.name;

            if (nextName != other.name) return;

            UpdateCurrentWayPoint(currentWayPoint);
        }
    }
}
