
// CURRENTLY TESTING! NOT TO BE USED YET!

using UnityEngine;
using VRTK;

public class CameraController : MonoBehaviour {

    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Transform[] cameraPoints;

    private VRTK_ControllerEvents events;

    private void Start() {

        events = GetComponent<VRTK_ControllerEvents>();
    }

    void Update() {

        SwitchCamera();
    }

    private void SwitchCamera() {

        if (events.buttonOnePressed) {

            for (int i = 0; i < cameraPoints.Length; i++) {

                if (i != cameraPoints.Length) {

                    SetCameraPos(i);

                } else {

                    SetCameraPos(0); // prevent array overflow
                }
            }
        }
    }

    private void SetCameraPos(int i) {

        cameraPivot.SetParent(cameraPoints[i], false);
        //cameraPivot.parent = cameraPoints[i];
        //cameraPivot.position = Vector3.zero;
    }
}
