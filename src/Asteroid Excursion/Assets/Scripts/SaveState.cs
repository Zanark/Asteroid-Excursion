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

    [XmlElement("currency")]
    public int currency;

    [XmlElement("completedLevel")]
    public int completedLevel;
    public SaveState()
    {
        colorOwned = 0;
        trailOwned = 0;
        currency = 0;
        completedLevel = 0;
    }
}
