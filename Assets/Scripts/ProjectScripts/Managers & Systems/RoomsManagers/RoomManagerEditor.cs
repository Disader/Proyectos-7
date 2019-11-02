using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]
public class RoomManagerEditor : MonoBehaviour
{
    [HideInInspector] public RoomManager roomManager;
    [HideInInspector] public BoxCollider2D myCollider;
    public LayerMask m_LayerMask;
    [HideInInspector] public Vector3 m_center;
    [HideInInspector] public Vector3 m_dimensions;

   [SerializeField] List<EnemyControl_MovementController> pruebadeListas = new List<EnemyControl_MovementController>();

    private void Awake()
    {
        roomManager = GetComponent<RoomManager>();
        myCollider = GetComponent<BoxCollider2D>();
        m_dimensions = myCollider.size;
        m_center = myCollider.offset;
    }


    public void CalculateEnemies()
    {
        Debug.Log("calculateEnemies");

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position + m_center, m_dimensions, 0f, m_LayerMask);
        int i = 0;

        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            EnemyControl_MovementController enemyController = hitColliders[i].GetComponent<EnemyControl_MovementController>();
            if (enemyController != null)
            {
              
            }
            i++;
        }
    }


    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode

        Gizmos.DrawWireCube(gameObject.transform.position + m_center, m_dimensions);
    }
}

[CustomEditor(typeof(RoomManagerEditor))]
public class RoomManagerButton : Editor
{

    RoomManagerEditor[] myScripts;

    private void OnEnable()
    {
        myScripts = FindObjectsOfType<RoomManagerEditor>();
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        List<EnemyControl_MovementController> values = new List<EnemyControl_MovementController>();
        SerializedProperty sp = serializedObject.FindProperty("pruebadeListas");

        if (GUILayout.Button("Calculate all enemies' room"))
        {
            values.Clear();
            foreach (RoomManagerEditor room in myScripts)
            {
                //room.CalculateEnemies();
                Collider2D[] hitColliders = Physics2D.OverlapBoxAll(room.m_center, room.m_dimensions, 0f, room.m_LayerMask);
                int i = 0;

                //Check when there is a new collider coming into contact with the box
                while (i < hitColliders.Length)
                {
                    EnemyControl_MovementController enemyController = hitColliders[i].GetComponent<EnemyControl_MovementController>();
                    if (enemyController != null)
                    {
                        if (sp.isArray)
                        {
                            int arrayLength = 0;

                            sp.Next(true); // skip generic field
                            sp.Next(true); // advance to array size field

                            // Get the array size
                            arrayLength = sp.intValue;

                            sp.Next(true); // advance to first array index

                            // Write values to list
                           
                            int lastIndex = arrayLength - 1;
                            for (int z = 0; z < arrayLength; z++)
                            {
                                values.Add(enemyController); // copy the value to the list
                                if (z < lastIndex) sp.Next(false); // advance without drilling into children
                            }

                            // iterate over the list displaying the contents
                            for (int b = 0; b < values.Count; b++)
                            {
                                EditorGUILayout.LabelField(b + " = " + values[b]);
                            }
                        }
                    }
                   
                    i++;
                }
            }
           
            serializedObject.ApplyModifiedProperties();
        }
    }
   
    private static bool WriteSerialzedProperty(SerializedProperty sp, object variableValue)
    {
        // Type the property and fill with new value
        SerializedPropertyType type = sp.propertyType; // get the property type

        if (type == SerializedPropertyType.Integer)
        {
            int it = (int)variableValue;
            if (sp.intValue != it)
            {
                sp.intValue = it;
            }
        }
        else if (type == SerializedPropertyType.Boolean)
        {
            bool b = (bool)variableValue;
            if (sp.boolValue != b)
            {
                sp.boolValue = b;
            }
        }
        else if (type == SerializedPropertyType.Float)
        {
            float f = (float)variableValue;
            if (sp.floatValue != f)
            {
                sp.floatValue = f;
            }
        }
        else if (type == SerializedPropertyType.String)
        {
            string s = (string)variableValue;
            if (sp.stringValue != s)
            {
                sp.stringValue = s;
            }
        }
        else if (type == SerializedPropertyType.Color)
        {
            Color c = (Color)variableValue;
            if (sp.colorValue != c)
            {
                sp.colorValue = c;
            }
        }
        else if (type == SerializedPropertyType.ObjectReference)
        {
            Object o = (Object)variableValue;
            if (sp.objectReferenceValue != o)
            {
                sp.objectReferenceValue = o;
            }
        }
        else if (type == SerializedPropertyType.LayerMask)
        {
            int lm = (int)variableValue;
            if (sp.intValue != lm)
            {
                sp.intValue = lm;
            }
        }
        else if (type == SerializedPropertyType.Enum)
        {
            int en = (int)variableValue;
            if (sp.enumValueIndex != en)
            {
                sp.enumValueIndex = en;
            }
        }
        else if (type == SerializedPropertyType.Vector2)
        {
            Vector2 v2 = (Vector2)variableValue;
            if (sp.vector2Value != v2)
            {
                sp.vector2Value = v2;
            }
        }
        else if (type == SerializedPropertyType.Vector3)
        {
            Vector3 v3 = (Vector3)variableValue;
            if (sp.vector3Value != v3)
            {
                sp.vector3Value = v3;
            }
        }
        else if (type == SerializedPropertyType.Rect)
        {
            Rect r = (Rect)variableValue;
            if (sp.rectValue != r)
            {
                sp.rectValue = r;
            }
        }
        else if (type == SerializedPropertyType.ArraySize)
        {
            int aSize = (int)variableValue;
            if (sp.intValue != aSize)
            {
                sp.intValue = aSize;
            }
        }
        else if (type == SerializedPropertyType.Character)
        {
            int ch = (int)variableValue;
            if (sp.intValue != ch)
            {
                sp.intValue = ch;
            }
        }
        else if (type == SerializedPropertyType.AnimationCurve)
        {
            AnimationCurve ac = (AnimationCurve)variableValue;
            if (sp.animationCurveValue != ac)
            {
                sp.animationCurveValue = ac;
            }
        }
        else if (type == SerializedPropertyType.Bounds)
        {
            Bounds bounds = (Bounds)variableValue;
            if (sp.boundsValue != bounds)
            {
                sp.boundsValue = bounds;
            }
        }
        else
        {
            Debug.Log("Unsupported SerializedPropertyType \"" + type.ToString() + " encoutered!");
            return false;
        }
        return true;
    }
}
