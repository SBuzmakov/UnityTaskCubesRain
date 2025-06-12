using Source.Scripts.Objects;
using Source.Scripts.Services;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class CountViewPresenter<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
    {
        [SerializeField] private ObjectsCounter<T> _objectsCounter;
        [SerializeField] private CountViewer _createdObjectsViewer;
        [SerializeField] private CountViewer _spawnedObjectsViewer;
        [SerializeField] private CountViewer _activeObjectsViewer;

        public void OnEnable()
        {
            _objectsCounter.ChangedCreatedCount += _createdObjectsViewer.SetCountView;
            _objectsCounter.ChangedSpawnedCount += _spawnedObjectsViewer.SetCountView;
            _objectsCounter.ChangedActiveCount += _activeObjectsViewer.SetCountView;
        }

        public void OnDisable()
        {
            _objectsCounter.ChangedCreatedCount -= _createdObjectsViewer.SetCountView;
            _objectsCounter.ChangedSpawnedCount -= _spawnedObjectsViewer.SetCountView;
            _objectsCounter.ChangedActiveCount -= _activeObjectsViewer.SetCountView;
        }

        public void Initialize()
        {
            _createdObjectsViewer.SetCountView(_objectsCounter.CreatedCount);
            _spawnedObjectsViewer.SetCountView(_objectsCounter.SpawnedCount);
            _activeObjectsViewer.SetCountView(_objectsCounter.ActiveCount);
        }
    }
}