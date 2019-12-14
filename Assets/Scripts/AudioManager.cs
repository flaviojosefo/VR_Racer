using UnityEngine;

public class AudioManager : MonoBehaviour {

    #region Singleton
    public static AudioManager Instance { get; private set; }

    private void Awake() {

        if (Instance != null && Instance != this) {

            Destroy(gameObject);

        } else {

            Instance = this;
        }
    }
    #endregion

    [SerializeField] private AudioClip[] carSounds;

    public AudioSource AudioSource { get; private set; }

    void Start() {

        AudioSource = GetComponent<AudioSource>();
    }

    public void SwitchAudio() {


    }
}
