FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine
WORKDIR /App

# Copy everything
COPY . ./

ENTRYPOINT ["dotnet", "run"]
CMD [ "Program.cs" ]
