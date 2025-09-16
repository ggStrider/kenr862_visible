using System;
using Definitions.GameItems.Scripts;
using Internal.Core.Audio;
using Internal.Core.DataModel;
using TMPro;
using UnityEngine;
using Zenject;

namespace Internal.Gameplay.GameTasks
{
    // hardcoded cuz its last day, and im in hurry
    public class HardCodedShitNamedGameTasks : MonoBehaviour
    {
        [SerializeField] private GameTask _makeCowGoToBathroom;
        [SerializeField] private GameTask _collectAllToiletPaper;
        [SerializeField] private GameTask _stealMonitor;
        [SerializeField] private GameTask _takeStapler;
        
        [Header("Feel")] 
        [SerializeField] private AudioPlayer _onTaskComplete;
        [SerializeField] private AudioPlayer _onAllTasksCompleted;
        
        private PlayerData _playerData;

        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void _CompletePigGoToBathroom()
        {
            _onTaskComplete.PlayRandomShot();
            _makeCowGoToBathroom.IsCompleted = true;
            _makeCowGoToBathroom.TaskPlaceholder.fontStyle = FontStyles.Strikethrough;
            _makeCowGoToBathroom.TaskPlaceholder.color = Color.white;

            DoActionIfAllCompleted();
        }

        public void _CompleteCollectAllToiletPaper()
        {
            _onTaskComplete.PlayRandomShot();
            _collectAllToiletPaper.IsCompleted = true;
            _collectAllToiletPaper.TaskPlaceholder.fontStyle = FontStyles.Strikethrough;
            _collectAllToiletPaper.TaskPlaceholder.color = Color.white;
            
            DoActionIfAllCompleted();
        }

        public void _CompleteStealMonitor()
        {
            _onTaskComplete.PlayRandomShot();
            _stealMonitor.IsCompleted = true;
            _stealMonitor.TaskPlaceholder.fontStyle = FontStyles.Strikethrough;
            _stealMonitor.TaskPlaceholder.color = Color.white;
            
            DoActionIfAllCompleted();
        }
        
        public void _CompleteTakeStapler()
        {
            _onTaskComplete.PlayRandomShot();
            _takeStapler.IsCompleted = true;
            _takeStapler.TaskPlaceholder.fontStyle = FontStyles.Strikethrough;
            _takeStapler.TaskPlaceholder.color = Color.white;
            
            DoActionIfAllCompleted();
        }

        public bool AreAllCompleted()
        {
            var allCompleted = _makeCowGoToBathroom.IsCompleted
                               && _collectAllToiletPaper.IsCompleted
                               && _stealMonitor.IsCompleted
                               && _takeStapler.IsCompleted;

            return allCompleted;
        }

        private void DoActionIfAllCompleted()
        {
            if (AreAllCompleted())
            {
                OnAllTasksCompleted();
            }
        }

        private void OnAllTasksCompleted()
        {
            _onAllTasksCompleted.PlayRandomShot();
        }
    }
    
    [Serializable]
    public class GameTask
    {
        public string TaskText;
        public TextMeshPro TaskPlaceholder;

        public bool IsCompleted;
    }
}