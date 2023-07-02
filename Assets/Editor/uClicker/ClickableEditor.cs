using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace uClicker.Editor
{
    [CustomEditor(typeof(Clickable))]
    public class ClickableEditor : UnityEditor.Editor
    {
        ClickerComponentEditorUtils editorUtils = new ClickerComponentEditorUtils();

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LanguageMetadata.AutofillLanguageMetaButton(target as ScriptableObject);

            editorUtils.CreateEditorSprawl(target);
        }
    }
}