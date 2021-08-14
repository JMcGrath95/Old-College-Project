using DaveCore.UI.Tooltips;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace DaveCore.UI.Objectives
{

    public class QuestToolTipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            throw new System.NotImplementedException();
        }

    }

}
