namespace ScribanSpike
{
    static class Helper
    {
        public static string GetTemplate()
        {
            return @"
		        <h2 dir='ltr'>Job Title: {sTitle}</h2>
            <p dir='ltr'>
              <strong>Department X:</strong> {lBrandID}<br />
              <strong>Department:</strong>{lDepartmentID}<br />
              <strong>Location:</strong> {lLocationID}<br />
              <strong>Site:</strong>{lSiteID}
            </p>

            <h2 dir='ltr'>Position Description Title: {{ PositionDescriptionTitle }}</h2>
            <h2 dir='ltr'>Version Number: {{ VersionNumber }}</h2>
            {{ #FlexibleData.posGroup.posDetails }}
              <h2 dir='ltr'>Position:</h2>
              <ul>
                <li><strong>Title: </strong> {{title}}</li>
                <li><strong>Full-time equivalent: </strong>{{positionProperties.fte}}</li>
                <li><strong>Seniority: </strong>{{ positionProperties.seniority }}</li>
                <li><strong>Brand Name: </strong>{{ positionProperties.brand Name }}</li>
                <li><strong>Department Name: </strong>{{ positionProperties.department Name }}</li>
              </ul>
            {{ /FlexibleData.posGroup.posDetails }}
            {{ #FlexibleData.dutiesGroup.duties }}
              <h2 dir='ltr'>Duty:</h2>
              <ul>
                <li><strong>Type: </strong> {{dutyType}}</li>
                <li><strong>Description: </strong>{{dutyDescription}}</li>
                <li><strong>Percentage Allocation: </strong>{{percentageAllocation}}</li>
              </ul>
            {{ /FlexibleData.dutiesGroup.duties }}
            {{ #FlexibleData.capabilityGroup.capabilities }}
              <h2 dir='ltr'>Capability:</h2>
              <ul>
                <li><strong>Name: </strong> {{capabilityName}}</li>
                <li><strong>Requirement Level: </strong>{{requirementLevel}}</li>
              </ul>
            {{ /FlexibleData.capabilityGroup.capabilities }}
            {{ #FlexibleData.fundingSourcesGroup.fundingSources }}
              <h2 dir='ltr'>Funding Source:</h2>
              <ul>
                <li><strong>GL Number: </strong> {{glNumber}}</li>
                <li><strong>Percentage Distribution: </strong>{{percentageDistribution}}</li>
              </ul>
            {{ /FlexibleData.fundingSourcesGroup.fundingSources }}
            <h2 dir='ltr'>Physical Demands:</h2>
            <p dir='ltr'>
              <strong>Physical Option:</strong> {{ FlexibleData.PhysicalDemands.physicalOption }}<br />
            </p>

            <h3 dir='ltr'>Payment Information</h3>
            <p dir='ltr'>
              <strong>Employment Type:</strong> {lWorkTypeID}<br />
              <strong>Job Duties:</strong>{JobDuties}<br />
              <strong>Pay scale type:</strong> {lPayScaleTypeID}
            </p>
            <h3 dir='ltr'>Other Requirements</h3>
            <p dir='ltr'>
              <span style='text-decoration: underline'><em><strong>Faster than a speeding bullet:</strong></em></span>{bOther1}<br />
              <em><span style='text-decoration: underline'><strong>Able to leap tall buildings in a single bound:</strong></span></em>{bOther2}
            </p>
            <h4 dir='ltr'><span style='color: #ff0000'>Additional Duties:</span></h4>
            <p dir='ltr'>
              <span
                style='
                  font-family: 'arial black', sans-serif;
                  font-size: 18pt;
                  background-color: #000000;
                  color: #00ccff;
                '
                >Team Name: {sOther1}</span
              >
            </p>
            <p dir='ltr'>
              <span
                style='
                  font-family: 'arial black', sans-serif;
                  font-size: 18pt;
                  background-color: #993366;
                '
                >Team Colour: {sOther10}</span
              >
            </p>
            <p dir='ltr'>
              <span style='font-size: 10pt; font-family: arial, helvetica, sans-serif'
                >Terms and Conditions</span
              >
            </p>
            <p dir='ltr'>
              <span style='font-size: 10pt; font-family: arial, helvetica, sans-serif'
                >Spicy jalapeno bacon ipsum dolor amet beef ribs spare ribs ut, capicola et
                ball tip short ribs rump. Frankfurter cupidatat veniam meatball consectetur
                ham hock do short loin ball tip pariatur. Sunt magna meatball deserunt swine
                eiusmod reprehenderit incididunt flank salami aliqua. Ad shoulder nisi beef
                ribs pork loin capicola swine short ribs aliquip short loin.</span
              >
            </p>
            <p dir='ltr'>
              <span style='font-size: 14pt'>Join our awesome team!&nbsp;</span>
            </p>
            <h2 dir='ltr'>
              <img
                style='border-width: initial; border-image-width: initial'
                src='https://lh5.googleusercontent.com/ttTq3FTMyVM7A7UTufgCA2nFnRsg8fvkmJTkNCPZrdsSMe8ypmO3W7BL-C5DGHnHpdB-WQhZ-5UP_CZqcIMjAwL7FusAadCT6Ldq4dUWqZ00ugUUcBCNjoNwgsrtpKYrRyP7jf0F'
                width='476'
                height='317'
              />
            </h2>
            <p>&nbsp;</p";
        }
    }
}