using System;
using System.Collections.Generic;
using Stubble.Core.Builders;

namespace ScribanSpike
{
	class Program
	{
		static void Main(string[] args)
		{
			var templateText = @"
	        <table>
				<tr>
					<th>{{TableName}}</th>
					<th>{{TableLevel}}</th>
				</tr>
				{{ #CapabilityGroup.Capabilities }}
					<tr>
						<td>{{ CapabilityName }}</td>
						<td>{{ RequirementLevel }}</td>
					</tr>
				{{ /CapabilityGroup.Capabilities }}
			</table>
		<hr/>
		 <table>
{{CapabilityGroupList}}
						<tr>
							<th>Name</th>
							<th>Level</th>
						</tr>
						{{ #CapabilityGroup2.Capabilities }}
							<tr>
								<td>{{ CapabilityName }}</td>
								<td>{{ RequirementLevel }}</td>
							</tr>
						{{ /CapabilityGroup2.Capabilities }}
					</table>
			";

			{

				var localPd = new PD
				{
					TableName = "Test Name",
					TableLevel = "Test Level",
					CapabilityGroup = new CapabilityGroup
					{
						Capabilities = new List<Capability>
						{
							new Capability {CapabilityName = "NextGen1", RequirementLevel = "High"},
							new Capability {CapabilityName = "NextGen2", RequirementLevel = "Low"}
						}
					},
					CapabilityGroup2 = new CapabilityGroup
					{
						Capabilities = new List<Capability>
						{
							new Capability {CapabilityName = "NextGen3", RequirementLevel = "High"},
							new Capability {CapabilityName = "NextGen4", RequirementLevel = "Low"}
						}
					}
				};

				var stubble = new StubbleBuilder().Build();
				var result = stubble.Render(templateText, localPd);

				Console.WriteLine(result);
			}
		}
	}

	public class PD {
		public string TableName { get; set; }
		public string TableLevel { get; set; }
		public CapabilityGroup CapabilityGroup { get; set; }
		public CapabilityGroup CapabilityGroup2 { get; set; }
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