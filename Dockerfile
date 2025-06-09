# Giai đoạn 1: Build ứng dụng
# Sử dụng .NET SDK image làm base image cho giai đoạn build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Đặt thư mục làm việc bên trong container
WORKDIR /app

# Copy file .csproj và restore dependencies
# Bước này cache các package NuGet nếu file .csproj không đổi
COPY *.csproj ./
RUN dotnet restore

# Copy toàn bộ mã nguồn còn lại vào thư mục làm việc
COPY . ./

# Publish ứng dụng ra thư mục 'out'
RUN dotnet publish "ql.csproj" -c Release -o /app/out --no-restore

# Giai đoạn 2: Chạy ứng dụng
# Sử dụng .NET Runtime image làm base image cho giai đoạn chạy
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Đặt thư mục làm việc (không bắt buộc nhưng tốt cho tổ chức)
WORKDIR /app

# Copy output từ giai đoạn build vào thư mục làm việc của giai đoạn runtime
COPY --from=build /app/out .

# Mở cổng mà ứng dụng ASP.NET Core lắng nghe (mặc định là 80 trong container)
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development
# Thiết lập điểm vào (entry point) của container
ENTRYPOINT ["dotnet", "ql.dll"]