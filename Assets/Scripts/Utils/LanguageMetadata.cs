using System;
using UnityEngine;

[System.Serializable]
public struct LanguageMetadata
{
    public string name;
    public string description;
    public string verb;
    public string verbPast;

    [SerializeField]
    private string plural;

    public string getPlural()
    {
        if (plural == null || plural == "")
        {
            return name + "s";
        }

        return plural;
    }

    [SerializeField]
    private string pastActedOn;

    public string getPastActedOn()
    {
        if (pastActedOn == null || pastActedOn == "")
        {
            return getPlural() + " " + verbPast;
        }

        return pastActedOn;
    }


    // Some editor stuff
    public static void AutofillLanguageMetaButton(ScriptableObject building)
    {
        if (!(building is IHasLanguageMetadata))
        {
            // Add in a warning label to the UI
            GUILayout.Label("This object does not implement IHasLanguageMetadata");
            return;
        }

        if (GUILayout.Button("Autofill Language Metadata"))
        {
            LanguageMetadata metadata = (building as IHasLanguageMetadata).languageMetadata;
            metadata.name = building.name;
            metadata.description = building.name + " description";
            // metadata.verb = "Buy";
            // metadata.verbPast = "Bought";
            (building as IHasLanguageMetadata).languageMetadata = metadata;
        }
    }
}

public interface IHasLanguageMetadata
{
    LanguageMetadata languageMetadata { get; set; }
}