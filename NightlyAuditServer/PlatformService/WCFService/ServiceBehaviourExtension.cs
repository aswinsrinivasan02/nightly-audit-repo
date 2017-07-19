using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Configuration;

namespace BallyTech.UI.Web.Platform.WebService
{
    public class ServiceBehaviourExtension : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(CustomHeader); }
        }

        protected override object CreateBehavior()
        {
            return new CustomHeader();
        }
    }
}
