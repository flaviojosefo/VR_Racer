using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private Transform camerasParent;
    private List<GameObject> cameras;

    private int currentCamera = 0;

    private void Start() {

        cameras = new List<GameObject>();

        StartCoroutine(GetCameras());
    }

    void Update() {

        SwitchCamera();
    }

    private IEnumerator GetCameras() {

        yield return new WaitForSeconds(1);

        cameras.Add(Camera.main.gameObject);

        foreach (Transform t in camerasParent) {

            cameras.Add(t.gameObject);
        }
    }

    private void SwitchCamera() {

        if (Input.GetKeyDown(KeyCode.E) || OVRInput.GetDown(OVRInput.Button.One)) {

            switch (currentCamera) {

                case 0:
                    cameras[0].SetActive(false);
                    cameras[1].SetActive(true);
                    currentCamera++;
                    break;

                case 1:
                    cameras[1].SetActive(false);
                    cameras[2].SetActive(true);
                    currentCamera++;
                    break;

                case 2:
                    cameras[2].SetActive(false);
                    cameras[0].SetActive(true);
                    currentCamera = 0;
                    break;
            }
        }   
    }
}
