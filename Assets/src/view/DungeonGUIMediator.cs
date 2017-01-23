using strange.extensions.mediation.impl;
using trailmarch.model.api;

namespace trailmarch.view
{
    public class DungeonGUIMediator : Mediator
    {
        [Inject]
        public DungeonGUIView view { get; set; }

        [Inject]
        public IResourceManager resourceMgr { get; set; }

        public override void OnRemove()
        {
            view.Dispose();
        }

        public override void OnRegister()
        {
            resourceMgr.LoadResource("dungeon_ui", "ui/DungeonUI");

            view.Init();
        }
    }
}
