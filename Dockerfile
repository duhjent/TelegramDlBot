FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /App
RUN apt -y update
RUN apt install curl python3 ffmpeg -y
RUN curl -L -o /usr/local/bin/yt-dlp https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp && chmod +x /usr/local/bin/yt-dlp
COPY --from=build /App/out .
ENTRYPOINT ["dotnet", "TelegramDlBot.Worker.dll"]