using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Environment
{
    artic,
    desert,
    tropical
}

public enum humid
{
    wet,
    moise,
    dry
}
public enum tempature
{
    hot,
    cold,
    warm
}


public class Augment : MonoBehaviour
{
    public humid humid;
    public tempature temp;
    public Environment environment;
}