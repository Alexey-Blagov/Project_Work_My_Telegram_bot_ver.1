# ������� �����
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# �������� ����� �������
COPY . ./

# �������� ����������
# RUN dotnet publish -c Release -o publish_self

# ��������� �����
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish_self .
ENTRYPOINT ["dotnet", "Project_Work_My_Telegram_bot.dll"]