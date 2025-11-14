using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(ImageLoadTextureAddin), false)]
//[CanEditMultipleObjects]
//public class ResourcePathEditor : Editor
//{
//    SerializedProperty Path;

//    string curPath = "";

//    private void OnEnable()
//    {
//        Path = serializedObject.FindProperty("ImageTexturePath");
//    }

//    public override void OnInspectorGUI()
//    {
//        //获得一个长500的框  
//        var mExcelPathRect = EditorGUILayout.GetControlRect(GUILayout.Width(500));
//        //EditorGUI.TextField(mExcelPathRect, curPath);
//        //如果鼠标正在拖拽中或拖拽结束时，并且鼠标所在位置在文本输入框内  
//        if ((Event.current.type == EventType.DragUpdated || Event.current.type == EventType.DragExited) && mExcelPathRect.Contains(Event.current.mousePosition))
//        {
//            //改变鼠标的外表  
//            DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
//            if (DragAndDrop.paths != null && DragAndDrop.paths.Length > 0)
//            {
//                string retPath = DragAndDrop.paths[0];
//                curPath = retPath;
//            }
//        }
//        Path.stringValue = curPath;
//        serializedObject.ApplyModifiedProperties();
        
//    }
//}