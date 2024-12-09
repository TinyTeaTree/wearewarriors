using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class SceneSwitcherTool : EditorWindow
{
    // Array to hold references to scene assets
    private static SceneAsset[] sceneShortcuts;

    // Maximum number of shortcut scenes
    private const int MAX_SCENES = 12;

    [InitializeOnLoadMethod]
    static void Initialize()
    {
        // Initialize the scene shortcuts array
        sceneShortcuts = new SceneAsset[MAX_SCENES];

        // Register the keyboard event handler
        SceneView.duringSceneGui += HandleShortcuts;
    }

    // Custom editor window for configuring scene shortcuts
    [MenuItem("Tools/Scene Switcher")]
    public static void ShowWindow()
    {
        GetWindow<SceneSwitcherTool>("Scene Switcher");
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Scene Shortcut Configuration", EditorStyles.boldLabel);
        
        // Create object fields for each scene shortcut
        for (int i = 0; i < MAX_SCENES; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"F{i + 1} Shortcut:", GUILayout.Width(100));
            sceneShortcuts[i] = (SceneAsset)EditorGUILayout.ObjectField(sceneShortcuts[i], typeof(SceneAsset), false);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.HelpBox("Configure your scene shortcuts. Assign scene assets to use Cntr/Alt/Option+1/2/3 for quick scene switching.", MessageType.Info);
    }

    static void HandleShortcuts(SceneView sceneView)
    {
        // Check for Command/Control key modifier
        bool isModifierDown = (Event.current.modifiers & EventModifiers.Alt) != 0 ||
                               (Event.current.modifiers & EventModifiers.Control) != 0;
        

        // Check for function key presses (F1-F12)
        if (isModifierDown && Event.current.type == EventType.KeyDown)
        {
            for (int i = 0; i < MAX_SCENES; i++)
            {
                if (Event.current.keyCode == KeyCode.Alpha1 + i)
                {
                    // Check if a scene is assigned to this shortcut
                    if (sceneShortcuts[i] != null)
                    {
                        string scenePath = AssetDatabase.GetAssetPath(sceneShortcuts[i]);
                        
                        // Check if there are unsaved changes
                        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                        {
                            // Open the selected scene
                            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                            
                            // Mark the event as used to prevent further processing
                            Event.current.Use();
                        }
                        break;
                    }
                }
            }
        }
    }

    // Save the scene shortcuts between Unity sessions
    void OnEnable()
    {
        // Load saved scene references
        for (int i = 0; i < MAX_SCENES; i++)
        {
            string savedScenePath = EditorPrefs.GetString($"SceneSwitcherScene_{i}", "");
            if (!string.IsNullOrEmpty(savedScenePath))
            {
                sceneShortcuts[i] = AssetDatabase.LoadAssetAtPath<SceneAsset>(savedScenePath);
            }
        }
    }

    void OnDisable()
    {
        // Save scene references
        for (int i = 0; i < MAX_SCENES; i++)
        {
            if (sceneShortcuts[i] != null)
            {
                EditorPrefs.SetString($"SceneSwitcherScene_{i}", AssetDatabase.GetAssetPath(sceneShortcuts[i]));
            }
            else
            {
                EditorPrefs.DeleteKey($"SceneSwitcherScene_{i}");
            }
        }
    }
}