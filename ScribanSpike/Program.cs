﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
				FlexibleData = getFlexibleData(), // Transform(getFlexibleData()),
				RequestId = new Guid("b61e3e2f-22b9-4b7b-9f20-2c083fc75f57")
			};
			
			// Create StubblePDModel based on the public PD.
			// - Navigate through the whole PD object tree
			// StubblePDModel properties, including Flexible Data properties, should be string or number.
			// - Before assigning each value to the StubblePDModel, convert to string or number accordingly.
			// Pass StubblePDModel into the engine.
			// =================or==================
			// Try serializing the whole thing before passing-in
			
			var stubble = new StubbleBuilder().Build();
			var result = stubble.Render(
				templateText,
				publicPD
			);
			
			Console.WriteLine(result);
		}
		
		static JObject getFlexibleData()
		{
			var json = File.ReadAllText(@"C:\code\ScribanSpike\ScribanSpike\flexibleData.json");
			// var json = "{ \"posGroup\": { \"posDetails\": [{ \"id\": \"15508d98-9430-6c19-21fe-df79910c5ff6\", \"title\": \"Receptionist\", \"externalId\": \"185522\", \"viewableReference\": \"B39855\", \"positionProperties\": { \"fte\": \"Temporary\", \"seniority\": \"Entry Level\", \"brand Name\": \"Abc corp\", \"department Name\": \"Product\" } }] }, \"dutiesGroup\": { \"duties\": [{ \"dutyType\": \"Essential\", \"dutyDescription\": \"some duty\", \"percentageAllocation\": 25 }, { \"dutyType\": \"Essential\", \"dutyDescription\": \"some duty\", \"percentageAllocation\": 25 }] }, \"capabilityGroup\": { \"capabilities\": [{ \"capabilityName\": \"Software\", \"requirementLevel\": \"Mandatory\" }] }, \"physicalDemands\": { \"physicalOption\": false }, \"positionDetails\": { \"salary\": 100000, \"classification\": \"Professional\", \"skillsKnowledge\": \"skills\" }, \"backgroundChecks\": { }, \"fundingSourcesGroup\": { \"fundingSources\": [{ \"glNumber\": \"10-00-7-15000-6210\", \"percentageDistribution\": 25 }] }, \"decisionMakingSection\": { } }";
			return JObject.Parse(json);
		}
		
		static JObject Transform(JObject targetObject)
		{
			JObject resultObject = new JObject();
			var jProperties = targetObject.Properties().ToList();
			foreach (var jProperty in jProperties)
			{
				if (jProperty.Value.Type == JTokenType.Array)
				{
					resultObject[jProperty.Name] = ProcessArray((JArray)jProperty.Value);
				}
				else if (jProperty.Value.Type == JTokenType.Object)
				{
					resultObject[jProperty.Name] = Transform((JObject)jProperty.Value);
				}
				else
				{
					resultObject[jProperty.Name] = jProperty.Value;
					if (jProperty.Value.Type == JTokenType.Boolean)
					{
						resultObject[jProperty.Name] = jProperty.Value.ToString();
					}
				}
			}

			return resultObject;
		}
		
		static JArray ProcessArray(JArray jArray)
		{
			var newArray = new JArray();
			foreach(var jToken in jArray.Where(x => x.Type == JTokenType.Object))
			{
				newArray.Add(Transform((JObject)jToken));
			}

			return newArray;
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