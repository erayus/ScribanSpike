using System;
using System.Collections.Generic;
using Stubble.Core.Builders;

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
				{{ #Capabilities }}
					<tr>
						<td>{{ CapabilityName }}</td>
						<td>{{ RequirementLevel }}</td>
					</tr>
				{{ /Capabilities }}
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
	        
	        var stubble = new StubbleBuilder().Build();
	        var result = stubble.Render(templateText, localPd.CapabilityGroup);
	        
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