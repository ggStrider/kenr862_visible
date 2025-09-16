using Internal.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace Internal.Gameplay.Interact.Features
{
    public class InteractAddItemAndThrowTutorial : InteractAddItemIntoInventory
    {
        [SerializeField, TextArea(3, 5)] private string _tutorialText;
        private UTutorialTextThrower _tutorialTextThrower;
        
        [Inject]
        private void Construct(UTutorialTextThrower tutorialTextThrower)
        {
            _tutorialTextThrower = tutorialTextThrower;
        }
        
        protected override void OnAdded()
        {
            _tutorialTextThrower.ThrowTutorial(_tutorialText);
        }
    }
}