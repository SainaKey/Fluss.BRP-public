using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MIDIAssignTogglePresenter : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private TMP_Text text;

    public Toggle Toggle
    {
        get { return this.toggle; }
    }

    public TMP_Text Text
    {
        get { return this.text; }
    }
}
