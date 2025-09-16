using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorTools
{
    public class LevelDesignBuildTool : EditorWindow
    {
        private float _worldHeight;
        private float _snapDelta = 0.25f;
        private bool _snapAlways;
        
        private int _selectedTab;
        private float _selectedObjectHeight;

        [MenuItem("Level Design/Build")]
        public static void ShowWindow()
        {
            GetWindow<LevelDesignBuildTool>("Build level");
        }
        
        private void OnSelectionChange()
        {
            Repaint();
        }
        
        private void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            if (Selection.activeTransform != null)
            {
                _selectedObjectHeight = Selection.activeTransform.position.y;
                if(_snapAlways) SnapSelectedObjects();
                
                Repaint();
            }
        }
        
        private void OnGUI()
        {
            GUILayout.Label("Level-design tools", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            string[] tabNames = { "Main", "Align & Rotate", "Mirror", "Collisions" };
            _selectedTab = GUILayout.Toolbar(_selectedTab, tabNames);
            GUILayout.Space(10);

            switch (_selectedTab)
            {
                case 0:
                    DrawHeightAndSnapControls();
                    DrawSetHeightAndSnapButtons();
                    ShowSelectedObjectHeight();
                    break;
                case 1:
                    DrawAlignWithViewButtons();
                    DrawRotationButtons();
                    break;
                case 2:
                    DrawMirrorButtons();
                    break;
                case 3:
                    DrawCollisionsManager();
                    break;
            }
        }

        private void ShowSelectedObjectHeight()
        {
            if (Selection.activeTransform != null)
            {
                _selectedObjectHeight = Selection.activeTransform.position.y;
                GUILayout.Label($"Selected Object Height: {_selectedObjectHeight}", EditorStyles.label);
            }
            else
            {
                GUILayout.Label("No object selected.", EditorStyles.label);
            }
        }

        private void DrawCollisionsManager()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add collision", GUILayout.Height(60), GUILayout.Width(175)))
            {
                foreach (var selected in Selection.transforms)
                {
                    if (selected.TryGetComponent<Collider>(out _))
                    {
                        Debug.LogWarning($"Already has a collider!");
                    }
                    else
                    {
                        TryAddComponentToObject<MeshCollider>(selected);
                    }
                }
            }
            
            if (GUILayout.Button("Remove collision", GUILayout.Height(60), GUILayout.Width(175)))
            {
                foreach (var selected in Selection.transforms)
                {
                    TryRemoveComponentFromObject<Collider>(selected);
                }
            }
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Select Objects Without Collider", GUILayout.Height(30)))
            {
                SelectObjectsWithoutCollider();
            }
            GUILayout.EndHorizontal();
        }

        private void SelectObjectsWithoutCollider()
        {
            var objectsWithoutCollider = new List<Object>();
    
            var allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
    
            foreach (var obj in allObjects)
            {
                if (!obj.GetComponent<Collider>())
                {
                    objectsWithoutCollider.Add(obj);
                }
            }

            Selection.objects = objectsWithoutCollider.ToArray();
        }

        private void TryAddComponentToObject<T>(Transform target) where T : Component
        {
            if (target.TryGetComponent<T>(out _))
            {
                Debug.LogWarning($"<color=yellow>Already has a {typeof(T).Name} on {target.name}</color>");
            }
            else
            {
                target.gameObject.AddComponent<T>();
                Debug.Log($"<color=green>Successful added {typeof(T).Name} on {target.name}</color>");
            }
        }
        
        private void TryRemoveComponentFromObject<T>(Transform target) where T : Component
        {
            if (!target.TryGetComponent<T>(out var component))
            {
                Debug.LogWarning($"<color=yellow>Don't have a {typeof(T).Name} on {target.name}</color>");
            }
            else
            {
                DestroyImmediate(component);
                Debug.Log($"<color=green>Successful removed {typeof(T).Name} from {target.name}</color>");
            }
        }

        private void DrawHeightAndSnapControls()
        {
            _worldHeight = EditorGUILayout.FloatField("Target Height", _worldHeight);
            _snapDelta = EditorGUILayout.FloatField("Snap Delta", _snapDelta);
            _snapAlways = EditorGUILayout.Toggle("Snap Always", _snapAlways);
            EditorGUILayout.Space();
        }

        private void DrawSetHeightAndSnapButtons()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Set height selected", GUILayout.Height(30)))
            {
                ApplyHeight();
            }
            if (GUILayout.Button("Snap Selected", GUILayout.Width(120), GUILayout.Height(30)))
            {
                SnapSelectedObjects();
            }
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Get height selected", GUILayout.Height(30)))
            {
                _worldHeight = Selection.activeTransform.position.y;
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Select All", GUILayout.Height(30)))
            {
                Selection.objects = FindObjectsByType<Object>(FindObjectsSortMode.None);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            DrawHorizontalLine();
        }

        private void DrawAlignWithViewButtons()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Align with View (position)", GUILayout.Height(30), GUILayout.Width(175)))
            {
                AlignWithView(false);
            }
            if (GUILayout.Button("Align with View (+rotation)", GUILayout.Height(30), GUILayout.Width(175)))
            {
                AlignWithView(true);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            DrawHorizontalLine();
        }

        private void DrawRotationButtons()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Rotate -45y", GUILayout.Height(30)))
            {
                RotateSelectedObjects(Vector3.up * -45f);
            }
            if (GUILayout.Button("Rotate +45y", GUILayout.Height(30), GUILayout.Width(180)))
            {
                RotateSelectedObjects(Vector3.up * +45f);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Rotate -90y", GUILayout.Height(30)))
            {
                RotateSelectedObjects(Vector3.up * -90f);
            }
            if (GUILayout.Button("Rotate +90y", GUILayout.Height(30), GUILayout.Width(180)))
            {
                RotateSelectedObjects(Vector3.up * +90f);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            DrawHorizontalLine();
        }

        private void DrawMirrorButtons()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Mirror x", GUILayout.Height(30)))
            {
                ScaleSelectedObjects(new Vector3(-1, 1, 1));
            }
            if (GUILayout.Button("Mirror y", GUILayout.Height(30), GUILayout.Width(90)))
            {
                ScaleSelectedObjects(new Vector3(1, -1, 1));
            }
            if (GUILayout.Button("Mirror z", GUILayout.Height(30), GUILayout.Width(90)))
            {
                ScaleSelectedObjects(new Vector3(1, 1, -1));
            }
            EditorGUILayout.EndHorizontal();
        }

        private void RotateSelectedObjects(Vector3 add)
        {
            foreach (var selected in Selection.transforms)
            {
                selected.rotation = Quaternion.Euler(selected.rotation.eulerAngles.x + add.x,
                    selected.rotation.eulerAngles.y + add.y, selected.rotation.eulerAngles.x + add.z);
            }
        }

        private void ScaleSelectedObjects(Vector3 delta)
        {
            foreach (var selected in Selection.transforms)
            {
                selected.localScale = new Vector3(
                    selected.localScale.x * delta.x,
                    selected.localScale.y * delta.y,
                    selected.localScale.z * delta.z);
            }
        }

        private void ApplyHeight()
        {
            foreach (var obj in Selection.transforms)
            {
                Undo.RecordObject(obj, "Set Height");
                obj.position = new Vector3(obj.position.x, _worldHeight, obj.position.z);
                EditorUtility.SetDirty(obj);
            }
        }

        private void SnapSelectedObjects()
        {
            foreach (var obj in Selection.transforms)
            {
                Undo.RecordObject(obj, "Snap Position");
                obj.position = new Vector3(
                    Mathf.Round(obj.position.x / _snapDelta) * _snapDelta,
                    Mathf.Round(obj.position.y / _snapDelta) * _snapDelta,
                    Mathf.Round(obj.position.z / _snapDelta) * _snapDelta
                );
                EditorUtility.SetDirty(obj);
            }
        }

        private void AlignWithView(bool alignRotation)
        {
            if (SceneView.lastActiveSceneView == null)
            {
                Debug.LogWarning("No active Scene View found.");
                return;
            }

            foreach (var obj in Selection.transforms)
            {
                Undo.RecordObject(obj, "Align with View");
                obj.position = SceneView.lastActiveSceneView.camera.transform.position;

                if (alignRotation)
                    obj.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;

                EditorUtility.SetDirty(obj);
            }
        }

        private void DrawHorizontalLine()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.Space();
        }
    }
}