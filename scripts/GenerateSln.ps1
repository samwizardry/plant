cd ..
dotnet new sln -n Plant -o .
dotnet sln .\Plant.sln add (ls -r .\src\*.csproj)
dotnet sln .\Plant.sln add (ls -r .\sandbox\*.csproj)
