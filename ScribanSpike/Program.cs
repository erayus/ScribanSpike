using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
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
			</table>";

			var flexibleData = @"";
			
			var publicPD = new PublicPositionDescription
			{
				PositionDescriptionId = new Guid("21ce5374-3d74-4a1a-aad7-4a0c4345fcb2"),
				PositionDescriptionTitle = "Position Description - Stubble",
				VersionNumber = 3,
				PositionIds = new List<string> { "195182", "195897" },
				PositionDescriptionUrl = "www.google.com",
				ProviderId = 10279,
				FlexibleData = {},
				RequestId = new Guid("b61e3e2f-22b9-4b7b-9f20-2c083fc75f57")
			};

			var stubble = new StubbleBuilder().Build();
			var result = stubble.Render(templateText, publicPD);

			Console.WriteLine(result);
		}
	}

	public class PublicPositionDescription
	{
		public Guid? PositionDescriptionId { get; set; }
        
		public string PositionDescriptionTitle { get; set; }
        
		public int? VersionNumber { get; set; }
        
		public List<string> PositionIds { get; set; }
        
		public string PositionDescriptionUrl { get; set; }
        
		public JObject FlexibleData { get; set; }
        
		public int? ProviderId { get; set; }
        
		public Guid? RequestId { get; set; }
	}
}