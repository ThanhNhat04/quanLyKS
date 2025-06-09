# ===== Step 1: Build & Publish =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Sao chép file .csproj và khôi phục gói
COPY *.csproj ./
RUN dotnet restore

# Sao chép toàn bộ mã nguồn và publish
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# ===== Step 2: Runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

# Copy từ build stage
COPY --from=build /app/publish .

# Mở cổng 80 (có thể chỉnh nếu WebAPI dùng cổng khác)
EXPOSE 80

# Khởi động API
ENTRYPOINT ["dotnet", "ql.dll"]
