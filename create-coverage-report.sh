##
# Generate test coverage report
##

# Preparation

# dotnet tool install -g dotnet-coverage
# dotnet tool install -g dotnet-reportgenerator-globaltool

# Cleanup and Rebuild

dotnet clean
dotnet build

# Execution

dotnet-coverage collect -f xml -o coverage.xml dotnet test
reportgenerator \
    -reports:'coverage.xml' \
    -targetdir:'coveragereport' \
    -assemblyfilters:'-*.Tests.dll'

# end
