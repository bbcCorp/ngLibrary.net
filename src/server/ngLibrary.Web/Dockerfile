# Stage 1
FROM microsoft/aspnetcore-build AS builder
LABEL authors="Bedabrata Chatterjee"

WORKDIR /source

ENV ClientApp__host localhost
ENV ClientApp__port 4200

# Note ":" in appsettings.json is replaced with "__"
ENV KLENS_ConnectionStrings__ngLibraryDbConnection $KLENS_ConnectionStrings__ngLibraryDbConnection

# caches restore result by copying csproj file separately
COPY *.csproj .
RUN dotnet restore

# copies the rest of your code
COPY . .
RUN dotnet publish --output /app/ --configuration Release

# Stage 3 Publish
FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=builder /app .
ENTRYPOINT ["dotnet", "ngLibrary.Web.dll"]
