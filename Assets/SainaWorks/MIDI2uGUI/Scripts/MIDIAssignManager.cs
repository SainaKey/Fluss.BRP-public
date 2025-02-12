using System;
using System.Collections;
using System.Collections.Generic;
using MIDI2uGUI;
using MidiJack;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MIDIAssignInfos
{
    public List<MIDIAssignInfo> MidiAssignInfoList = new List<MIDIAssignInfo>();
}

public class MIDIAssignManager : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public List<MIDIAssigner> midiAssignerList = new List<MIDIAssigner>();
    //public List<MIDIAssigner> midiAssigners = new List<MIDIAssigner>();

    private static MIDIAssignManager instance;

    private MIDIAssignManager () { // Private Constructor

        Debug.Log("Create MIDIAssignManager GameObject instance.");
    }

    public static MIDIAssignManager Instance {

        get {

            if( instance == null ) {

                GameObject go = new GameObject("MIDIAssignManager");
                instance = go.AddComponent<MIDIAssignManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        toggleGroup = gameObject.AddComponent<ToggleGroup>();
        toggleGroup.allowSwitchOff = true;
        MidiMaster.noteOnDelegate += NoteOn;
        MidiMaster.noteOffDelegate += NoteOff;
        MidiMaster.knobDelegate += Knob;
    }

    private void NoteOn(MidiChannel midiChannel, int noteNum, float velocity)
    {
        foreach (var midiAssigner in midiAssignerList)
        {
            midiAssigner.OnMIDISignal(midiChannel,noteNum,velocity);
        }
    }

    private void NoteOff(MidiChannel midiChannel, int noteNum)
    {
        foreach (var midiAssigner in midiAssignerList)
        {
            midiAssigner.OffMIDISignal(midiChannel,noteNum,0.0f);
        }
    }

    private void Knob(MidiChannel midiChannel, int knobNum, float knobValue)
    {
        foreach (var midiAssigner in midiAssignerList)
        {
            midiAssigner.OnMIDISignal(midiChannel,knobNum,knobValue);
        }
    }

    public void Save()
    {
        MIDIAssignInfos midiAssignInfos = new MIDIAssignInfos();
        foreach (var midiAssigner in midiAssignerList)
        {
            midiAssignInfos.MidiAssignInfoList.Add(midiAssigner.midiAssignInfo);
        }
        var jsonStr = JsonUtility.ToJson(midiAssignInfos, false);
        MidiAssignDataIO.OutputData(jsonStr);
    }

    public void Load()
    {
        var jsonStr = MidiAssignDataIO.InputJsonData();
        var data = JsonUtility.FromJson<MIDIAssignInfos>(jsonStr);
        foreach (var loadedMidiAssignInfo in data.MidiAssignInfoList)
        {
            foreach (var midiAssigner in midiAssignerList)
            {
                if (midiAssigner.midiAssignInfo.guid == loadedMidiAssignInfo.guid)
                {
                    midiAssigner.midiAssignInfo.midiInfos = loadedMidiAssignInfo.midiInfos;
                }
            }
        }
    }

    public void MappingModeOn()
    {
        foreach (var midiAssigner in midiAssignerList)
        {
            midiAssigner.MIDIMappingReadyModeOn();
        }
    }
    
    public void MappingModeOff()
    {
        foreach (var midiAssigner in midiAssignerList)
        {
            midiAssigner.MIDIMappingReadyModeOff();
        }
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.M) && Input.GetKey(KeyCode.LeftControl))
        {
            MappingModeOn();
        }
        

        
        if (Input.GetKeyDown(KeyCode.N) && Input.GetKey(KeyCode.LeftControl))
        {
            MappingModeOff();
        }
        

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            foreach (var midiAssigner in midiAssignerList)
            {
                midiAssigner.DeleteMIDI();
            }
        }
        
        
        if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.LeftControl))
        {
            Save();
        }
        

        
        if (Input.GetKeyDown(KeyCode.L) && Input.GetKey(KeyCode.LeftControl))
        {
            Load();
        }
        
    }
}
