using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.extensions.mediation.impl;
using trailmarch.consts;
using trailmarch.model.api;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace trailmarch.view
{
    public class DungeonGUIView : View, IDisposable
    {
        [Inject]
        public IResourceManager resourceMgr { get; set; }

        private GameObject _ui;

        public void Init()
        {
            _ui = (GameObject) Instantiate(resourceMgr.GetResource("dungeon_ui"), Vector3.zero, Quaternion.identity);
            _ui.transform.SetParent(transform);

            /*


            Canvas canvas;
            Image img;

            _canvasObj = new GameObject("Canvas");
            canvas = _canvasObj.AddComponent<Canvas>();
            _canvasObj.transform.SetParent(transform);

            _evtSystemObj = new GameObject("EventSystem");
            _evtSystemObj.AddComponent<EventSystem>();
            _evtSystemObj.transform.SetParent(transform);

            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            _test = new GameObject("img");
            _test.transform.SetParent(canvas.transform);
            img = _test.AddComponent<Image>();
            img.sprite = (resourceMgr.GetResource("ui_portrait2") as Sprite);
            
            img.rectTransform.sizeDelta = new Vector2(100, 100);
            img.rectTransform.anchoredPosition = new Vector2(50, 10);
            //_test = new GameObject("test");
            //_test.AddComponent<Label>();
            //_test.transform.parent = _canvasObj.transform;
            */
        }

        void OnGUI()
        {
            GUILayout.Label(GameSettings.VERSION);
        }

        public void Dispose()
        {
            Destroy(_ui);
        }
    }
}
