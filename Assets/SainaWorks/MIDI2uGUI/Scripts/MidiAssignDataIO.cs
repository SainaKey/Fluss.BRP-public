using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class MidiAssignDataIO
{
    private static string midiAssignDataPath = Application.dataPath + "/MidiAssignData.json";
    
    public static void OutputData(string jsonStr)
    {
        StreamWriter writer;
        
        writer = new StreamWriter(midiAssignDataPath, false);
        writer.Write (jsonStr);
        writer.Flush ();
        writer.Close ();
    }

    public static string InputJsonData()
    {
        if (!File.Exists(midiAssignDataPath)) return "";
        
        var json = File.ReadAllText(midiAssignDataPath);

        return json;
    }
}
