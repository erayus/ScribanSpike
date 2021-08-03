using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Stubble.Core.Builders;

namespace ScribanSpike
{
	class Program
	{
		static void Main(string[] args)
		{
			var templateText = Helper.GetTemplate();
			
			var publicPD = new PublicPositionDescription
			{
				PositionDescriptionId = new Guid("21ce5374-3d74-4a1a-aad7-4a0c4345fcb2"),
				PositionDescriptionTitle = "Position Description - Stubble",
				VersionNumber = 3,
				PositionIds = new List<string> { "195182", "195897" },
				PositionDescriptionUrl = "www.google.com",
				ProviderId = 10279,
				FlexibleData = getFlexibleData(),
				RequestId = new Guid("b61e3e2f-22b9-4b7b-9f20-2c083fc75f57")
			};

			var stubble = new StubbleBuilder().Build();
			var result = stubble.Render(templateText, publicPD.FlexibleData);

			Console.WriteLine(result);
		}
		static JObject getFlexibleData() {
			var json = "{ \"posGroup\": { \"posDetails\": [{ \"id\": \"15508d98-9430-6c19-21fe-df79910c5ff6\", \"title\": \"Receptionist\", \"externalId\": \"185522\", \"viewableReference\": \"B39855\", \"positionProperties\": { \"fte\": \"Temporary\", \"seniority\": \"Entry Level\", \"brand Name\": \"Abc corp\", \"department Name\": \"Product\" } }] }, \"dutiesGroup\": { \"duties\": [{ \"dutyType\": \"Essential\", \"dutyDescription\": \"some duty\", \"percentageAllocation\": 25 }, { \"dutyType\": \"Essential\", \"dutyDescription\": \"some duty\", \"percentageAllocation\": 25 }] }, \"capabilityGroup\": { \"capabilities\": [{ \"capabilityName\": \"Software\", \"requirementLevel\": \"Mandatory\" }] }, \"physicalDemands\": { \"physicalOption\": false }, \"positionDetails\": { \"salary\": 100000, \"classification\": \"Professional\", \"skillsKnowledge\": \"skills\" }, \"backgroundChecks\": { }, \"fundingSourcesGroup\": { \"fundingSources\": [{ \"glNumber\": \"10-00-7-15000-6210\", \"percentageDistribution\": 25 }] }, \"decisionMakingSection\": { } }";
			return JObject.Parse(json);
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