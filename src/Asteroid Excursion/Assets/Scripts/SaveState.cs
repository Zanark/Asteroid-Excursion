using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
[XmlRoot("SaveState")]
public class SaveState
{
    [XmlElement("colorOwned")]
    public int colorOwned;

    [XmlElement("trailOwned")]
    public int trailOwned;

    [XmlElement("activeColor")]
    public int activeColor;

    [XmlElement("activeTrail")]
    public int activeTrail;

    [XmlElement("currency")]
    public int currency;

    [XmlElement("completedLevel")]
    public int completedLevel;

    [XmlElement("usingAccelerometer")]
    public bool usingAccelerometer;

    public SaveState()
    {
        colorOwned = 0;
        trailOwned = 0;
        currency = 0;
        completedLevel = 0;
        activeColor = 0;
        activeTrail = 0;
        usingAccelerometer = true;
    }
}
