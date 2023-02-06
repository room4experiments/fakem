# Line 1: Use dotnet-framework base image
#FROM microsoft/dotnet-framework
#FROM mcr.microsoft.com/dotnet/runtime:6.0
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# Line 2: Download MCR installer (self-extracting executable) and save as ZIP file
ADD https://ssd.mathworks.com/supportfiles/downloads/R2020a/Release/7/deployment_files/installer/complete/win64/MATLAB_Runtime_R2020a_Update_7_win64.zip C:\\MATLAB_Runtime_R2020a_Update_7_win64.zip
# Line 3: Use PowerShell
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]
# Line 4: Unpack ZIP contents to installation folder
RUN Expand-Archive C:\\MATLAB_Runtime_R2020a_Update_7_win64.zip -DestinationPath C:\\MCR_INSTALLER
# Line 5: Run the setup command for a non-interactive installation of MCR
RUN Start-Process C:\MCR_INSTALLER\bin\win64\setup.exe -ArgumentList '-mode silent', '-agreeToLicense yes' -Wait
# Line 6: Remove ZIP and installation folder after setup is complete
RUN Remove-Item -Force -Recurse C:\\MCR_INSTALLER, C:\\MATLAB_Runtime_R2020a_Update_7_win64.zip

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat
#FROM mathworks/matlab:r2020b AS base
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["fakem/fakem.csproj", "fakem/"]
RUN dotnet restore "fakem/fakem.csproj"
COPY . .
WORKDIR "/src/fakem"
RUN dotnet build "fakem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "fakem.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fakem.dll"]