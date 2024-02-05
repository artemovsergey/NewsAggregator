# Техническое задание

Нужно написать REST API агрегатор новостей
1. Пользователь подает на вход адрес новостного сайта или его RSS-ленты
2. База данных агрегатора начинает пополняться новостями с этого сайта
3. У пользователя есть возможность просматривать список новостей из базы данных и искать их по подстроке в теле и/или в заголовке новости
Результат: Docker Compose, сделать миграции, не менее 2 рабочих адресов новостных сайтов для проверки
Стек: C#, PostgreSQL 14, EF, Docker

### Стек технологий:
- База данных: PostgreSQL
- ASP Core 8 (REST API)
- Entity Framework
- Material Design
- Angular
- Entity Framework
- Docker Compose


# Варианты использования

![](https://www.planttext.com/api/plantuml/svg/XL9BRi903DtFATwoNQ1UWJEK0rGZcDA8aA0mNLHL9JvYHHUzGQ-G0Ya1IhY2VQFQar8LeOfDndv-VdwsqXnwc_d7Vco6lIX1bWwTYTszXq-HHTq7ZqwKkRQFqYGm1ZQpVGniksUxhkMVOWSjswGREsFRFir0xrJHZbzfBU-2SfvGeRdv9OBUQCaZWPUXNF7eZlQ1NV4uy3TuDlXN2eq6hkWWu8foAdW2QSz12vuf1W5NgizZdEXglAGAF1R6HW3fAMb79OAN2hUjLaFoWlOyuVdrPGioHQJfImiIbpL5oW8Ug9HlHLkSBIV4DUX3NAnvZh35NcREUNPvfefkVwP8CeCU0lKrT81Q4a8Xvq3TgN5vSznq0T7DTLt9LJzm_peiBvi82ZpYANqUhPmeX37rVjEp_SoBQRgaet_TDm00)


# Docker Compose

```yml
version: '1'

networks:
  asp-dotnet-network:
    driver: bridge

services:

  angular:
    image: angular:latest
    build:
      context: ./NewsAggregator.Angular
      dockerfile: Dockerfile
    container_name: ContainerAngular
    ports:
      - "80:80"
    depends_on:
      - api
    networks:
      - asp-dotnet-network
      
  api:
    restart: always
    build:
      context: ./NewsAggregator.API
      dockerfile: Dockerfile
    container_name: ContainerAPI
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
    ports:
      - "8080:8080"
    networks:
      - asp-dotnet-network
    depends_on:
      - db

  db:
    image: postgres:latest
    container_name: ContainerPostgreSQL
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
      POSTGRES_DB: MirtekNewsAggregation
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
    networks:
      - asp-dotnet-network
volumes:
 postgres_data:
```

# Dockerfile Angular Nginx

```
FROM nginx:alpine
COPY ./dist /usr/share/nginx/html/
CMD ["nginx", "-g", "daemon off;"]
```

# Dockerfile ASP Core API
```
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY ["./publish", "."]

ENTRYPOINT ["dotnet", "NewsAggregator.API.dll"]
# ENTRYPOINT ["dotnet", "NewsAggregator.API.dll", "--urls", "http://localhost:7281"]
```


