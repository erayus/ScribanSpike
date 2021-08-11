using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stubble.Core.Builders;
using Stubble.Core.Contexts;
using Stubble.Core.Exceptions;
using Stubble.Core.Imported;
using Stubble.Core.Renderers.Interfaces;
using Stubble.Core.Settings;

namespace ScribanSpike
{
	class Program
	{
		static void Main(string[] args)
		{
			
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
			var publicPDJObject = JObject.Parse(JsonConvert.SerializeObject(publicPD));
			var templateText = Helper.GetTemplate();

			//Pre-process
			//Match everything between double curly braces
			string pattern = @"(?<={{|{{[#\/])(?:[a-zA-Z\d]*?)(?=}})";//@"(?<={{)\s?#?\/?\s?(.*?)\s?(?=}})";
			Regex rgx = new Regex(pattern);
			
			// Loop through each token
			foreach (Match matchToken in rgx.Matches(templateText))
			{
				var givenToken =  matchToken.Value;
				var fullTokenPathList = publicPDJObject.FindTokens(givenToken);

				if (fullTokenPathList.Count == 0)
				{
					Console.WriteLine($"{givenToken} is not found.");
					continue;
				}
				// if (fullTokenPathList.Count > 1)
				// {
				// 	//Same parent path
				// 	Console.WriteLine("Not yet consider: " + JsonConvert.SerializeObject(fullTokenPathList) );
				// }
				
				var fullTokenPath = fullTokenPathList[0].Path;
				Regex rgex = new Regex(@"(.*)\[\d\].(.*)");
				if (rgex.IsMatch(fullTokenPath))
				{
					foreach (Match match in rgex.Matches(fullTokenPath))
					{
						fullTokenPath = match.Groups[2].Value;
					}
				}
				templateText = Regex.Replace(templateText, @"(?<={{|{{|{{[#\/]|{{[#\/])(?:"+ givenToken + @")(?=}}|}})",fullTokenPath);
				// templateText = Regex.Replace(templateText, @"~!", "{{");
				// templateText = Regex.Replace(templateText, @"!~", "}}");
			}
			// Console.WriteLine(templateText);

			var stubble = new StubbleBuilder()
				.Build();
			
			var renderSettings = new RenderSettings
			{
				ThrowOnDataMiss = true
			};
			
			try
			{
				var result = stubble.Render(
					templateText, //Helper.GetOldTemplate(),
					publicPD,
					settings: renderSettings
				);
			
				Console.WriteLine(result);
			}
			catch (StubbleDataMissException e)
			{
				Console.WriteLine( e.Message.Replace("undefined", "not found."));
				// Case: field not found 
				// save error message (user friendly) to link table
			}
			catch (StubbleException e)
			{
				Console.WriteLine(e.Message);
				// Cases: open and closing tag mismatch,...
				// pre-process message to be a bit more friendly (e.g there is something wrong, it could be open and closing tags are not matched.
				// save to link table
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				// there's something wrong while trying to merge, contact support
				// save to link table
			}
		}
		
		static JObject getFlexibleData()
		{
			var json = File.ReadAllText(@"C:\workspace\ScribanSpike\ScribanSpike\flexibleData.json");
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