# Introduction
Meta - Datasources is a bachelors project created to extend the existing Meta - OKR Tracker.
The goal of this product is to explorer and make it easier for other UR Teams to create and add data sources to the OKR Tracker, especially with the focus on non-technical teams.

# Getting Started

## Prerequisites
1. [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
2. [Microsoft.Tye](https://github.com/dotnet/tye)
3. [NodeJS](https://docs.microsoft.com/en-us/windows/dev-environment/javascript/nodejs-on-windows)
4. [TailwindCSS](https://tailwindcss.com/docs/installation)  
   ```shell
   npm install -g tailwindcss
   ```

## Build and Test
1. Trust the development certificate:  
   ```shell
   dotnet dev-certs https --trust
   ```
3. Build the solution:  
   ```shell
   dotnet build
   ```
4. HotReload DatasourceApp:  
   ```shell
   dotnet watch --project ./src/DatasourceApp/DatasourceApp.csproj
   ```