using Cutscenes;
using Internal.Core;
using Internal.Gameplay.GameTasks;
using Internal.Gameplay.Hover.Features;
using Internal.Gameplay.Interact.Bases;
using Internal.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace Internal.Gameplay.Interact.Features
{
    public class InteractKeyCheckSpamButton : InteractKeyChecker
    {
        [SerializeField] private SpamInteractChangeScene _spamInteractButton;
        [SerializeField] private HoverToggleGameObject _hoverToggleGameObject;
        
        private bool _wasMatched;
        private HardCodedShitNamedGameTasks _shit;
        
        [Inject]
        private void Construct(CutsceneManager cutsceneManager)
        {
            _spamInteractButton.Initialize(cutsceneManager);
        }

        // im so sorry for this
        private void Awake() => _shit = FindAnyObjectByType<HardCodedShitNamedGameTasks>();
        
        protected override void OnKeyMatched()
        {
            if (!_shit.AreAllCompleted()) return;

            if (_wasMatched)
            {
                _spamInteractButton.PressedButton();
            }
            else
            {
                var hoverTarget = _hoverToggleGameObject.Target;
                Destroy(_hoverToggleGameObject);
                hoverTarget.SetActive(true);
                _spamInteractButton.ToggleDecreasing(active:true);
                _spamInteractButton.UI.SetActive(true);
                _wasMatched = true;
            }
        }
    }
}