using System;
using UnityEngine;

namespace uClicker
{
    [CreateAssetMenu(menuName = "uClicker/Clickable")]
    public class Clickable : ClickerComponent, IHasLanguageMetadata
    {
        public Currency Currency;
        public float Amount;

        [SerializeField]
        private LanguageMetadata _languageMetadata;
        LanguageMetadata IHasLanguageMetadata.languageMetadata { get => _languageMetadata; set => _languageMetadata = value; }
    }
}