using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFWizard.WF
{
    [Serializable]
    public class ActInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int  orderId { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
