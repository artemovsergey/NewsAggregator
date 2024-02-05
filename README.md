# Техническое задание

Нужно написать REST API агрегатор новостей
1. Пользователь подает на вход адрес новостного сайта или его RSS-ленты
2. База данных агрегатора начинает пополняться новостями с этого сайта
3. У пользователя есть возможность просматривать список новостей из базы данных и искать их по подстроке в теле и/или в заголовке новости
Результат: Docker Compose, сделать миграции, не менее 2 рабочих адресов новостных сайтов для проверки
Стек: C#, PostgreSQL 14, EF, Docker

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
