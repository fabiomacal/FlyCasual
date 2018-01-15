﻿using System;
using Upgrade;
using Ship;
using Abilities;
using Tokens;

namespace UpgradesList
{
    public class Bossk : GenericUpgrade
    {
        public Bossk() : base()
        {
            Type = UpgradeType.Crew;
            Name = "Bossk";
            Cost = 2;

            isUnique = true;

            UpgradeAbilities.Add(new BosskAbility());
        }

        public override bool IsAllowedForShip(GenericShip ship)
        {
            return ship.faction == Faction.Scum;
        }

    }
}

namespace Abilities
{
    public class BosskAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnAttackMissedAsAttacker += RegisterBosskAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackMissedAsAttacker -= RegisterBosskAbility;
        }

        private void RegisterBosskAbility()
        {
            if (!HostShip.HasToken(typeof(StressToken)))
            {
                RegisterAbilityTrigger(TriggerTypes.OnAttackMissed, PerformBosskAbility);
            }                
        }

        private void PerformBosskAbility(object sender, EventArgs e)
        {
            HostShip.AcquireTargetLock(Triggers.FinishTrigger);
            HostShip.AssignToken(new FocusToken(), Phases.CurrentSubPhase.FinishPhase);
            HostShip.AssignToken(new StressToken(), Phases.CurrentSubPhase.FinishPhase);
        }
    }    
}