using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.extensions.command.impl;
using trailmarch.actors;
using trailmarch.consts;
using trailmarch.controller.signals;
using trailmarch.vo;
using UnityEngine;

namespace trailmarch.controller.actors
{
    public class AttackCommand : Command
    {
        [Inject]
        public AttackGroup grp { get; set; }

        [Inject]
        public UpdateActorViewSignal updateActorView { get; set; }

        public override void Execute()
        {
            updateActorView.Dispatch(grp.Attacker, ActorActionType.Attack);
        }
    }
}
