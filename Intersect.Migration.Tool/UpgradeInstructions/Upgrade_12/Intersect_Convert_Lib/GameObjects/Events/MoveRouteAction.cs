﻿using System;

namespace Intersect.Migration.UpgradeInstructions.Upgrade_12.Intersect_Convert_Lib.GameObjects.Events
{
    public class MoveRouteAction
    {
        public Guid AnimationId { get; set; }
        public EventGraphic Graphic { get; set; }
        public MoveRouteEnum Type { get; set; }

        public MoveRouteAction Copy()
        {
            MoveRouteAction copy = new MoveRouteAction()
            {
                Type = Type
            };
            if (Type == MoveRouteEnum.SetGraphic)
            {
                copy.Graphic = new EventGraphic();
                copy.Graphic.CopyFrom(Graphic);
            }
            else if (Type == MoveRouteEnum.SetAnimation)
            {
                copy.AnimationId = AnimationId;
            }
            return copy;
        }
    }
}