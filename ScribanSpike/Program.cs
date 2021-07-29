using System;
using System.Collections.Generic;
using Scriban;

namespace ScribanSpike
{
    class Program
    {
        static void Main(string[] args)
        {
	        var templateText = @"<table>
				<tr>
					<th>Name</th>
					<th>Level</th>
				</tr>
				{{ for capability in _pd.CapabilityGroup.Capabilities}}
					<tr>
						<td>{{capability.CapabilityName}}</td>
						<td>{{capability.RequirementLevel}}</td>
					</tr>
				{{ end }}
			</table>
			";
	        var localPd = new PD
	        {
		        CapabilityGroup = new CapabilityGroup
		        {
			        Capabilities = new List<Capability>
			        {
				        new Capability {CapabilityName = "NextGen1", RequirementLevel = "High"},
				        new Capability {CapabilityName = "NextGen2", RequirementLevel = "Low"}
			        }
		        }
	        };
	        var template = Template.Parse(templateText);
            var result = template.Render(new { _pd = localPd});
            Console.WriteLine(result);
        }
    }
    
    public class PD {
		public CapabilityGroup CapabilityGroup { get; set; }
    }

    public class CapabilityGroup
    {
	    public CapabilityGroup()
	    {
		    Capabilities = new List<Capability>();
	    }
	    public  List<Capability> Capabilities { get; set; }
    }

    public class Capability
    {
	    public string CapabilityName { get; set; }
	    public string RequirementLevel { get; set; }
    }
}