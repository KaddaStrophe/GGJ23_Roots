using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GraphSystem.Model.OutcomeDecisionHandler
{
    [CreateAssetMenu(menuName = "Textadventure/OutcomeEnd")]
    public class OutcomeByEndHandler : A_OutcomeDecisionHandler
    {
        public OutcomeByEndHandler(Node node) : base(node) { }
    }
}
