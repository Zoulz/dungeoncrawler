using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using trailmarch.controller.actors;
using trailmarch.controller.actors.signals;
using trailmarch.controller.player.signals;
using trailmarch.controller.signals;
using trailmarch.controller.startup;
using trailmarch.controller.startup.signals;
using trailmarch.model;
using trailmarch.model.api;
using trailmarch.view;
using UnityEngine;

namespace trailmarch
{
    public class TrailmarchDungeonContext : MVCSContext
    {
        public TrailmarchDungeonContext(MonoBehaviour view) : base(view)
        {
        }

        public TrailmarchDungeonContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
		{
		}

        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        override public IContext Start()
        {
            base.Start();
            StartSignal startSignal = (StartSignal) injectionBinder.GetInstance<StartSignal>();
            startSignal.Dispatch();

            return this;
        }

        protected override void mapBindings()
        {
            //  Model
            injectionBinder.Bind<ILevelModel>().To<LevelModel>().ToSingleton();
            injectionBinder.Bind<IResourceManager>().To<ResourceManager>().ToSingleton();
            injectionBinder.Bind<IDefinitionModel>().To<DefinitionModel>().ToSingleton();

            //  View
            mediationBinder.Bind<LevelView>().To<LevelMediator>();
            mediationBinder.Bind<PlayerView>().To<PlayerMediator>();
            mediationBinder.Bind<InputHandlerView>().To<InputHandlerMediator>();
            mediationBinder.Bind<DungeonGUIView>().To<DungeonGUIMediator>();

            //  Controller
            commandBinder.Bind<StartSignal>().To<StartCommand>().Once();
            commandBinder.Bind<LoadMetaDataSignal>().To<LoadMetaDataCommand>().Once();
            commandBinder.Bind<LoadLevelSignal>().To<LoadLevelCommand>();
            commandBinder.Bind<MoveActorSignal>().To<MoveActorCommand>();
            commandBinder.Bind<MobAISignal>().To<MobAICommand>();
            commandBinder.Bind<AttackSignal>().To<AttackCommand>();
            commandBinder.Bind<EnableMovementSignal>();
            commandBinder.Bind<SpawnActorSignal>();
            commandBinder.Bind<UpdateActorViewSignal>();
            commandBinder.Bind<RedrawMapSignal>();
        }
    }
}
