using System;
using Internal.Core.Inputs;
using Internal.Core.UI;
using Internal.Core.UI.Bases;
using Internal.Gameplay.UI;
using KinematicCharacterController.Examples;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;

namespace Cutscenes
{
    // thats not manager at all tbh
    public class CutsceneManager : MonoBehaviour, IEscClosable
    {
        [SerializeField] private PlayableAsset _awakeCutscene;
        [SerializeField] private PlayableAsset _openExitDoorCutscene;

        [Space] [SerializeField] private PlayableDirector _director;
        [SerializeField] private bool _playOnStart = true;

        [Space, SerializeField] private string _textTutorialAfterFirstCutscene = 
            "<color=red>Highlighted items</color> are interactable with the <color=red>[F]</color> key.";

        private ExamplePlayer _examplePlayer;
        private InputReader _inputReader;
        private UTutorialTextThrower _tutorialTextThrower;
        private EscManager _escManager;

        private bool _wasCutscene;
        public event Action OnFirstCutsceneEnded;

        [Inject]
        private void Construct(ExamplePlayer player, UTutorialTextThrower tutorialTextThrower,
            InputReader inputReader, EscManager escManager)
        {
            _examplePlayer = player;
            _tutorialTextThrower = tutorialTextThrower;
            _inputReader = inputReader;
            _escManager = escManager;
        }

        private void Start()
        {
            if (_playOnStart)
            {
                _inputReader.DisablePlayerMap();
                _escManager.AddToStack(this);
                TriggerDirector(_awakeCutscene);
            }

            _director.stopped += OnPlayed;
        }

        private void OnDestroy()
        {
            _director.stopped -= OnPlayed;
        }

        public void PlayEndScene()
        {
            _inputReader.DisableAllMaps();
            TriggerDirector(_openExitDoorCutscene);
        }

        public void TriggerDirector(PlayableAsset asset)
        {
            _examplePlayer.enabled = false;

            _director.playableAsset = asset;
            _director.Play();
        }

        private void OnPlayed(PlayableDirector director)
        {
            _examplePlayer.enabled = true;

            // first cutscene
            if (!_wasCutscene)
            {
                _inputReader.EnableMapPlayer();
                _tutorialTextThrower.ThrowTutorial(_textTutorialAfterFirstCutscene);

                _wasCutscene = true;
                OnFirstCutsceneEnded?.Invoke();
                OnFirstCutsceneEnded = null;
            }
        }

        public void Close()
        {
            _director.Pause();
            
            _director.time = _director.duration;
            _director.Evaluate();
            
            _director.Stop();
            OnPlayed(_director);
        }
    }
}