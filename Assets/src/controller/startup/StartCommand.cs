using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.context.api;
using trailmarch.controller.startup.signals;
using trailmarch.model.api;
using trailmarch.view;

namespace trailmarch.controller.startup
{
    public class StartCommand : Command
    {
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView { get; set; }

		[Inject]
		public IResourceManager resourceMgr { get; set; }

        [Inject]
        public LoadMetaDataSignal loadMetaData { get; set; }

        [Inject]
        public LoadLevelSignal loadLevel { get; set; }

        public override void Execute()
        {
            //  Create dungeon map NavigationView.
            GameObject go = new GameObject("LevelView");
            go.AddComponent<LevelView>();
            go.transform.SetParent(contextView.transform);

            //  Create dungeon navigation NavigationView.
            go = new GameObject("PlayerView");
            go.AddComponent<PlayerView>();
            go.transform.SetParent(contextView.transform);

            go = new GameObject("InputHandlerView");
            go.AddComponent<InputHandlerView>();
            go.transform.SetParent(contextView.transform);

            go = new GameObject("DungeonGUIView");
            go.AddComponent<DungeonGUIView>();
            go.transform.SetParent(contextView.transform);

			resourceMgr.LoadAllResources("giantFootstep", "sounds/footsteps");

            //  Load ze resources.
            loadMetaData.Dispatch();
            loadLevel.Dispatch("testLevel");
        }
    }
}
