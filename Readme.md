# Setup  Database
## open package manager console
## select DemoProject.Domain as default project.

run command Add-Migration IdentityCreation -context DemoDbContext
after the above command run Update-Database -context DemoDbContext

# Run API
Set DemoProject.Api as Start up project
Run "IIS Express" in Visual Studio


# Future Improvements
More static code analytics using Visual Stuido, Resharper, SonarCube
Setup Build Pipeline
Setup NCrunch for realtime Unit Testing
Setup Automatic Code Coverage report in Pipeline
More unit test coverage to Reach 100% Coverage
