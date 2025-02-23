using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editors
{
    [CustomPropertyDrawer(typeof(Level.GoalSetup))]
    public class GoalSetupDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Rect fieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            SerializedProperty goalEnumProp = property.FindPropertyRelative("goalEnum");
            SerializedProperty maxPointsProp = property.FindPropertyRelative("maxPoints");
            SerializedProperty tileColorProp = property.FindPropertyRelative("tileColor");
            SerializedProperty tileQuantityProp = property.FindPropertyRelative("tileQuantity");

            EditorGUI.PropertyField(fieldRect, goalEnumProp);
            GoalsEnum currentGoal = (GoalsEnum)goalEnumProp.enumValueIndex;

            float yOffset = EditorGUIUtility.singleLineHeight + 2;
            fieldRect.y += yOffset;

            switch (currentGoal)
            {
                case GoalsEnum.TotalPointsGoal:
                case GoalsEnum.MaxMovesGoal:
                    EditorGUI.PropertyField(fieldRect, maxPointsProp);
                    break;

                case GoalsEnum.ColorTilesGoal:
                    EditorGUI.PropertyField(fieldRect, tileColorProp);
                    fieldRect.y += yOffset;
                    EditorGUI.PropertyField(fieldRect, tileQuantityProp);
                    break;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float totalHeight = EditorGUIUtility.singleLineHeight + 2;
            GoalsEnum currentGoal = (GoalsEnum)property.FindPropertyRelative("goalEnum").enumValueIndex;

            switch (currentGoal)
            {
                case GoalsEnum.TotalPointsGoal:
                case GoalsEnum.MaxMovesGoal:
                    totalHeight += EditorGUIUtility.singleLineHeight + 2;
                    break;
                case GoalsEnum.ColorTilesGoal:
                    totalHeight += (EditorGUIUtility.singleLineHeight + 2) * 2;
                    break;
            }

            return totalHeight;
        }
    }
}