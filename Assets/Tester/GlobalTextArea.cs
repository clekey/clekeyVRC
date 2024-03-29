﻿
using System;
using TMPro;
using UdonSharp;
using VRC.SDKBase;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class GlobalTextArea : UdonSharpBehaviour
{
    public TextMeshPro globalArea;
    public TextMeshPro sourceArea;
    public TextMeshPro headerLine;

    [UdonSynced]
    [NonSerialized]
    public string text;

    private void Update()
    {
        if (Networking.IsOwner(gameObject))
        {
            if (text != sourceArea.text)
            {
                text = sourceArea.text;
                RequestSerialization();
            }
        }

        globalArea.text = text;
        var owner = Networking.GetOwner(gameObject);
        headerLine.text = $"Global Textarea(owner: {(owner == null ? "None" : owner.displayName)})";
    }

    public override void Interact()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
    }
}
