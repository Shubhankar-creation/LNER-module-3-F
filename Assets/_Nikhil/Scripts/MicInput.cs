using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicInput : MonoBehaviour
{
    public GameObject shoutUI;
    [SerializeField]
    private float minLoudness = 0;
    [SerializeField]
    private List<PlayerMovement> npcCharacters = new List<PlayerMovement>();
/*    public Slider volume;
*/
    AudioClip _clipRecord;

    int _sampleWindow = 128;
    private bool _isInitialized;
    private bool _isRecording = false;
    private float MicLoudnessinDecibels;
    private string _device;

    private void Start()
    {
        RecordVoice();
    }
    public void InitMic()
    {
        if (_device == null)
        {
            _device = Microphone.devices[0];
            Debug.Log(Microphone.devices[0]);
        }
        _clipRecord = Microphone.Start(_device, true, 10, 44100);
        _isInitialized = true;
    }

    public void StopMicrophone()
    {
        Microphone.End(_device);
        _isInitialized = false;
    }

    float MicrophoneLevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

    float MicrophoneLevelMaxDecibels(float loudness)
    {
        float db = 20 * Mathf.Log10(Mathf.Abs(loudness));
        return db;
    }

    void Update()
    {
        if(_isRecording)
        {
            if(_isInitialized)
            {
                MicLoudnessinDecibels = MicrophoneLevelMax();
                MicLoudnessinDecibels = MicrophoneLevelMaxDecibels(MicLoudnessinDecibels);
                if(MicLoudnessinDecibels > minLoudness)
                {
                    _isRecording = false;
                    foreach (var npc in npcCharacters)
                    {
                        npc.Shouted();
                        shoutUI.SetActive(false);
                    }
                }
            }
        }
        /*Debug.Log(MicLoudnessinDecibels);
        volume.value = (180 + MicLoudnessinDecibels) / 180;*/
    }

    void OnDisable() => StopMicrophone();
    void OnDestroy() => StopMicrophone();
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (!_isInitialized)
            {
                InitMic();
            }
        }
        if (!focus)
        {
            StopMicrophone();
        }
    }

    public void RecordVoice()
    {
        _isRecording = true;
        InitMic();
    }
}