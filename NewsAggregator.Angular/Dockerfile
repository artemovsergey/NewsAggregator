# Используйте официальный образ Nginx для размещения вашего приложения
FROM nginx:alpine

# Копируйте собранные файлы вашего приложения в папку html сервера Nginx
COPY ./dist /usr/share/nginx/html/

# Запустите Nginx
CMD ["nginx", "-g", "daemon off;"]
