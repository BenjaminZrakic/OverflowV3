using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Dialogue{

    
    //[SerializeField] public TextAsset dialogueFile;

    public string name;
    public string dialogueFileName;

    [HideInInspector]
    public string filePath; 
    
    [HideInInspector]
    public string[] sentences;
    
    
    
}

