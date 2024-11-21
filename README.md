архитектура 
схема базы
user flow
security
deploy
user guide

### Физическая модель базы данных

![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/schemes/DB.jpg)

# Проектное руководство

## Пользовательский интерфейс

### User Flow диаграммы

**User Flow** – это схема движения пользователя, наглядный разветвлённый сценарий его взаимодействия с конкретным цифровым продуктом: приложением или сайтом. User Flow показывает точки входа в сценарий, все переходы и страницы на пути к достижению пользователем его цели. 

Проработка User Flow – важный этап проектирования интерфейса: от того, насколько понятным и быстрым будет путь пользователя, зависит удобство и простота конечного продукта [1]. 

User Flow для пользователя и администратора представлены на рисунках 1.1 – 1.2 соответственно.

![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/user-flow/user-flow.jpg)

### Рисунок 1.1 – User Flow для пользователя

Диаграмма представляет собой описание последовательности действий пользователя при взаимодействии с системой, начиная с авторизации и заканчивая выбором основных функций. 

1. **Авторизация:**  
   Пользователь вводит данные для входа, и система проверяет их на корректность.  
   - Если данные неверные, отображается сообщение об ошибке.  
   - Если данные верны, пользователь переходит к следующему этапу – выбору пункта меню.  

2. **Выбор бизнес-процессов:**  
   После успешной авторизации пользователь может выбирать из нескольких ключевых функций:  
   - Работа с кошельком (управление средствами);  
   - Продажа и покупка;  
   - Конвертация монет.  

Каждый процесс соответствует конкретной функции системы и использует элементы интерфейса, которые обозначены цифрами под блоками.  

Диаграмма визуализирует логику пользовательского взаимодействия с системой через интерфейс, подчеркивая основные функциональные модули. Она помогает понять, как пользователи могут взаимодействовать с различными модулями системы через интуитивные интерфейсы.

![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/user-flow/admin-flow.jpg)

### Рисунок 1.2 – User Flow для администратора

Диаграмма описывает взаимодействие администратора с системой, начиная с процесса авторизации и продолжая выбором доступных функций управления. 

1. **Авторизация:**  
   Администратор запускает процесс с начальной точки – **Старт**, после чего переходит на страницу авторизации.  
   - На этом этапе вводятся учетные данные, и система проверяет их на корректность.  
   - Если данные неверные, выводится уведомление об ошибке, и администратор должен снова попытаться авторизоваться.  
   - При успешной проверке данных процесс продолжается, и администратор получает доступ к функциям системы.  

2. **Выбор ключевых функций:**  
   После успешной авторизации администратор попадает на этап выбора меню, где представлены функции управления:  
   - Управление монетами;  
   - Управление пользователями.  

Каждый процесс сопровождается цифровой маркировкой, указывающей на использование конкретных интерфейсов системы.  


### Примеры экранов UI

![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/10%D0%BE.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/11%D0%B0.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/12%D0%BA.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/13%D0%BF.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/15%D1%82.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/17%D1%83.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/18%D1%80.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/19%D0%B8.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/20%D0%B2.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/21%D0%BF.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/22%D0%B4.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/23%D0%B4.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/2%D1%80.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/3%D0%B3.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/4%D0%B8.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/5%D0%BA.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/6%D0%BF.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/7%D0%BA.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/8%D1%81.jpg)
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/pages/9%D0%BA.jpg)

## Безопасность

Для обеспечения надежности системы реализованы следующие меры безопасности:

## Аутентификация через JWT

**JSON Web Token (JWT)** используется для управления аутентификацией и авторизацией. Основные преимущества использования JWT:  
- **Безопасность**: Токен шифруется, что позволяет передавать данные безопасно.  
- **Гибкость**: JWT работает независимо от платформы и может быть интегрирован в любые системы.  

#### Принцип работы:
1. После успешной авторизации пользователя сервер генерирует JWT.  
2. Токен содержит:  
   - Заголовок (**Header**): указывает тип токена и алгоритм шифрования (например, HS256).  
   - Полезная нагрузка (**Payload**): данные пользователя (например, `userId`, `role`) без конфиденциальной информации.  
   - Подпись (**Signature**): шифруется с использованием секрета сервера.  

Пример структуры токена:
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c

3. При запросах к защищенным ресурсам клиент отправляет токен в заголовке `Authorization`.  
4. Сервер проверяет подпись токена и определяет права доступа пользователя.  

---

## Хэширование паролей с использованием соли

Пароли пользователей никогда не хранятся в системе в открытом виде. Применяются следующие методы защиты:  

1. **Соль (Salt)**: уникальная случайная строка, добавляемая к паролю перед его хэшированием. Это предотвращает использование радужных таблиц для взлома.  
2. **Хэширование**: для преобразования пароля используется алгоритм (например, bcrypt или Argon2).  

#### Процесс хэширования:
1. Генерация случайной соли.
2. Добавление соли к паролю.
3. Хэширование результата.
4. Сохранение хэша и соли в базе данных.  

Пример:
- Пароль: `mypassword`  
- Соль: `r4nd0mS4lt`  
- Хэш: `bcrypt(mypassword + r4nd0mS4lt) -> $2b$12$...`  

Эти меры обеспечивают, что даже в случае утечки данных злоумышленник не сможет восстановить пароли пользователей.

---

## Развёртывание
# Docker Compose для развертывания приложения

## Структура `docker-compose.yml`

```yaml
version: '3.8'
services:
  db:
    image: mysql:8.0
    container_name: mysql-db
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: root
      MYSQL_DATABASE: appdb
      MYSQL_USER: root
      MYSQL_PASSWORD: root
    ports:
      - "3306:3306"
    volumes:
      - db_data:/var/lib/mysql

  api:
    image: mcr.microsoft.com/dotnet/aspnet:8.0
    container_name: aspnet-api
    build:
      context: ./api
      dockerfile: Dockerfile
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Server=db;Database=appdb;User=appuser;Password=apppassword;
    ports:
      - "5000:80"
    depends_on:
      - db

  client:
    image: node:18-alpine
    container_name: react-client
    build:
      context: ./client
      dockerfile: Dockerfile
    ports:
      - "3000:3000"
    volumes:
      - ./client:/app
      - /app/node_modules
    depends_on:
      - api
volumes:
  db_data:

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "UP.dll"]

FROM node:18-alpine
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
EXPOSE 3000
CMD ["npm", "start"]

