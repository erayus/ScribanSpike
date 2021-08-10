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
            {{ #posDetails }}
              <h2 dir='ltr'>Position:</h2>
              <ul>
                <li><strong>Title: </strong> ~!title!~</li>
                <li><strong>Full-time equivalent: </strong>~!positionProperties.fte!~</li>
                <li><strong>Seniority: </strong>~!positionProperties.seniority!~</li>
                <li><strong>Brand Name: </strong>~!positionProperties.brand Name!~</li>
                <li><strong>Department Name: </strong>~!positionProperties.department Name!~</li>
              </ul>
            {{ /posDetails }}
            {{ #capabilities }}
              <h2 dir='ltr'>Capability:</h2>
              <ul>
                <li><strong>Name: </strong> ~!capabilityName!~</li>
                <li><strong>Requirement Level: </strong>~!requirementLevel!~</li>
              </ul>
            {{ /capabilities }}
             <p dir='ltr'>
              <strong>Physical Option:</strong> {{ physicalOption }}<br />
            </p>
        ";
      }
      
      public static string GetOldTemplate()
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
                <li><strong>Title: </strong>{{ title }}</li>
                <li><strong>Full-time equivalent: </strong>{{ positionProperties.fte }}</li>
                <li><strong>Seniority: </strong>{{positionProperties.seniority}}</li>
                <li><strong>Brand Name: </strong>{{positionProperties.brand Name}}</li>
                <li><strong>Department Name: </strong>{{positionProperties.department Name}}</li>
              </ul>
            {{ /FlexibleData.posGroup.posDetails }}
            {{ #FlexibleData.capabilityGroup.capabilities }}
              <h2 dir='ltr'>Capability:</h2>
              <ul>
                <li><strong>Name: </strong> {{capabilityName}}</li>
                <li><strong>Requirement Level: </strong>{{requirementLevel}}</li>
              </ul>
            {{ /FlexibleData.capabilityGroup.capabilities }}
            <p dir='ltr'>
              <strong>Physical Option:</strong> {{ FlexibleData.physicalDemands.physicalOption }}<br />
            </p>
        ";
      }
    }
}