# Архитектура

## Физическая модель базы данных
![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/schemes/DB.jpg)

## Архитектура в нотации C4

![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/schemes/DB.jpg](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/staff/%D0%A14_1.jpg)\)
### Рисунок 1.1 – Второй уровень С4-модели


![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/schemes/DB.jpg](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/staff/%D0%A14_2.jpg)
### Рисунок 1.2 – Компонентный уровень модели С4

## UML-диаграммы

- схема конвертации криптовалюты

- диаграмма развертывания

# Проектное руководство

# Пользовательский интерфейс

## User Flow диаграммы

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

### Развертывание всех служб:

docker-compose up --build

### Остановка и удаление контейнеров:

docker-compose down

### Остановка контейнеров без их удаления:

docker-compose stop
```

### Документация

## Документация к API

```json

{
  "openapi": "3.0.1",
  "info": {
    "title": "UP API",
    "version": "v1"
  },
  "paths": {
    "/Admin/blockUser": {
      "post": {
        "tags": [
          "Admin"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          },
          {
            "name": "reason",
            "in": "query",
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Admin/deleteUser": {
      "post": {
        "tags": [
          "Admin"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Admin/setStatusDel": {
      "put": {
        "tags": [
          "Admin"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          },
          {
            "name": "status",
            "in": "query",
            "schema": {
              "type": "boolean"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Admin/setStatusBlock": {
      "put": {
        "tags": [
          "Admin"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          },
          {
            "name": "status",
            "in": "query",
            "schema": {
              "type": "boolean"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Admin/getUserList": {
      "get": {
        "tags": [
          "Admin"
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Admin/getUserById": {
      "get": {
        "tags": [
          "Admin"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Admin/get-all-coins": {
      "get": {
        "tags": [
          "Admin"
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Admin/get-active-coins": {
      "get": {
        "tags": [
          "Admin"
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Admin/get-active-coins-dict": {
      "get": {
        "tags": [
          "Admin"
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Admin/set-coin-status": {
      "patch": {
        "tags": [
          "Admin"
        ],
        "parameters": [
          {
            "name": "coinName",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "status",
            "in": "query",
            "schema": {
              "type": "boolean"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Admin/getToken/{email}": {
      "post": {
        "tags": [
          "Admin"
        ],
        "parameters": [
          {
            "name": "email",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Authorization/login": {
      "post": {
        "tags": [
          "Authorization"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/AuthenticationRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/AuthenticationRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/AuthenticationRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Authorization/register": {
      "post": {
        "tags": [
          "Authorization"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/RegisterRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/RegisterRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/RegisterRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Currency/getUserCoins": {
      "get": {
        "tags": [
          "Currency"
        ],
        "parameters": [
          {
            "name": "userId",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Currency/getUserCoinsFull": {
      "get": {
        "tags": [
          "Currency"
        ],
        "parameters": [
          {
            "name": "userId",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Currency/getQuantityAfterConversion": {
      "get": {
        "tags": [
          "Currency"
        ],
        "parameters": [
          {
            "name": "shortNameStart",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "shortNameFinal",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "quantity",
            "in": "query",
            "schema": {
              "type": "number",
              "format": "double"
            }
          },
          {
            "name": "userId",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Currency/getUserBalance": {
      "get": {
        "tags": [
          "Currency"
        ],
        "parameters": [
          {
            "name": "userId",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Currency/getCoinQuantityInUserWallet": {
      "get": {
        "tags": [
          "Currency"
        ],
        "parameters": [
          {
            "name": "userId",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          },
          {
            "name": "coinName",
            "in": "query",
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Currency/getCoinPrice": {
      "get": {
        "tags": [
          "Currency"
        ],
        "parameters": [
          {
            "name": "quantity",
            "in": "query",
            "schema": {
              "type": "number",
              "format": "double"
            }
          },
          {
            "name": "coinName",
            "in": "query",
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Currency/get-coin-price-history/{shortName}": {
      "get": {
        "tags": [
          "Currency"
        ],
        "parameters": [
          {
            "name": "shortName",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Currency/getCurrenciesList": {
      "get": {
        "tags": [
          "Currency"
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Email/sendVerificationCode": {
      "post": {
        "tags": [
          "Email"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/SendVerificationCodeRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/SendVerificationCodeRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/SendVerificationCodeRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Email/verifyEmail": {
      "post": {
        "tags": [
          "Email"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/VerifyEmailRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/VerifyEmailRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/VerifyEmailRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Email/confirm-restore-password": {
      "post": {
        "tags": [
          "Email"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/VerifyEmailRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/VerifyEmailRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/VerifyEmailRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Email/restore-password": {
      "patch": {
        "tags": [
          "Email"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/RestorePasswordRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/RestorePasswordRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/RestorePasswordRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Email/send-message-block": {
      "patch": {
        "tags": [
          "Email"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/SendMessageRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/SendMessageRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/SendMessageRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Service": {
      "get": {
        "tags": [
          "Service"
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Transaction/getCoinQuantity/{coinName}/{quantityUSD}": {
      "get": {
        "tags": [
          "Transaction"
        ],
        "parameters": [
          {
            "name": "coinName",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "quantityUSD",
            "in": "path",
            "required": true,
            "schema": {
              "type": "number",
              "format": "double"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Transaction/getUserConversationsHistory/{id}": {
      "get": {
        "tags": [
          "Transaction"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Transaction/getUserDepositHistory/{id}": {
      "get": {
        "tags": [
          "Transaction"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Transaction/convert": {
      "post": {
        "tags": [
          "Transaction"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/ConvertRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/ConvertRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/ConvertRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Transaction/buyCrypto": {
      "post": {
        "tags": [
          "Transaction"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/BuyCryptoRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/BuyCryptoRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/BuyCryptoRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Transaction/sellCrypto": {
      "put": {
        "tags": [
          "Transaction"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/SellCryptoRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/SellCryptoRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/SellCryptoRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Transaction/sendCrypto": {
      "post": {
        "tags": [
          "Transaction"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/SendCryptoRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/SendCryptoRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/SendCryptoRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Transaction/replenishTheBalance": {
      "post": {
        "tags": [
          "Transaction"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/ReplenishTheBalanceRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/ReplenishTheBalanceRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/ReplenishTheBalanceRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Transaction/withdrawUSDT": {
      "put": {
        "tags": [
          "Transaction"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/WithdrawRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/WithdrawRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/WithdrawRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Transaction/getUserWithdrawalsHistory/{userId}": {
      "get": {
        "tags": [
          "Transaction"
        ],
        "parameters": [
          {
            "name": "userId",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/Transaction/getUserTransactionsHistory/{userId}": {
      "get": {
        "tags": [
          "Transaction"
        ],
        "parameters": [
          {
            "name": "userId",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/User/editUserLogin": {
      "put": {
        "tags": [
          "User"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/EditUserLoginRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/EditUserLoginRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/EditUserLoginRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/User/editUserPassword": {
      "put": {
        "tags": [
          "User"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/EditUserPasswordRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/EditUserPasswordRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/EditUserPasswordRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/User/editUserEmail": {
      "put": {
        "tags": [
          "User"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/EditUserEmailRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/EditUserEmailRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/EditUserEmailRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/User/editUser": {
      "put": {
        "tags": [
          "User"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/EditUserRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/EditUserRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/EditUserRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/User/getUserLoginHistory/{id}": {
      "get": {
        "tags": [
          "User"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/User/getUserLoginById": {
      "get": {
        "tags": [
          "User"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/User/deleteAccount": {
      "delete": {
        "tags": [
          "User"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/User/changeLogin": {
      "put": {
        "tags": [
          "User"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          },
          {
            "name": "newLogin",
            "in": "query",
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/User/changePassword": {
      "put": {
        "tags": [
          "User"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/ChangePasswordRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/ChangePasswordRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/ChangePasswordRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/User/CreateUserAsync": {
      "post": {
        "tags": [
          "User"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateUserRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateUserRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/CreateUserRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/User/IsEmailUniqueAsync": {
      "post": {
        "tags": [
          "User"
        ],
        "parameters": [
          {
            "name": "email",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "id",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "uuid"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "boolean"
                }
              },
              "application/json": {
                "schema": {
                  "type": "boolean"
                }
              },
              "text/json": {
                "schema": {
                  "type": "boolean"
                }
              }
            }
          }
        }
      }
    },
    "/User/IsLoginUniqueAsync": {
      "post": {
        "tags": [
          "User"
        ],
        "parameters": [
          {
            "name": "login",
            "in": "query",
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "boolean"
                }
              },
              "application/json": {
                "schema": {
                  "type": "boolean"
                }
              },
              "text/json": {
                "schema": {
                  "type": "boolean"
                }
              }
            }
          }
        }
      }
    },
    "/User/IsEmailValid": {
      "post": {
        "tags": [
          "User"
        ],
        "parameters": [
          {
            "name": "email",
            "in": "query",
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "boolean"
                }
              },
              "application/json": {
                "schema": {
                  "type": "boolean"
                }
              },
              "text/json": {
                "schema": {
                  "type": "boolean"
                }
              }
            }
          }
        }
      }
    }
  },
  "components": {
    "schemas": {
      "AuthenticationRequest": {
        "type": "object",
        "properties": {
          "login": {
            "type": "string",
            "nullable": true
          },
          "password": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "BuyCryptoRequest": {
        "type": "object",
        "properties": {
          "userId": {
            "type": "string",
            "format": "uuid"
          },
          "coinName": {
            "type": "string",
            "nullable": true
          },
          "quantity": {
            "type": "number",
            "format": "double"
          }
        },
        "additionalProperties": false
      },
      "ChangePasswordRequest": {
        "type": "object",
        "properties": {
          "id": {
            "type": "string",
            "format": "uuid"
          },
          "password": {
            "type": "string",
            "nullable": true
          },
          "passwordRepeat": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "ConvertRequest": {
        "type": "object",
        "properties": {
          "shortNameStart": {
            "type": "string",
            "nullable": true
          },
          "shortNameFinal": {
            "type": "string",
            "nullable": true
          },
          "quantity": {
            "type": "number",
            "format": "double"
          },
          "userId": {
            "type": "string",
            "format": "uuid"
          }
        },
        "additionalProperties": false
      },
      "CreateUserRequest": {
        "type": "object",
        "properties": {
          "id": {
            "type": "string",
            "format": "uuid"
          },
          "nickname": {
            "type": "string",
            "nullable": true
          },
          "password": {
            "type": "string",
            "nullable": true
          },
          "ipAddress": {
            "type": "string",
            "nullable": true
          },
          "applicationId": {
            "type": "string",
            "format": "uuid"
          }
        },
        "additionalProperties": false
      },
      "EditUserEmailRequest": {
        "type": "object",
        "properties": {
          "id": {
            "type": "string",
            "format": "uuid"
          },
          "email": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "EditUserLoginRequest": {
        "type": "object",
        "properties": {
          "id": {
            "type": "string",
            "format": "uuid"
          },
          "login": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "EditUserPasswordRequest": {
        "type": "object",
        "properties": {
          "id": {
            "type": "string",
            "format": "uuid"
          },
          "password": {
            "type": "string",
            "nullable": true
          },
          "passwordRepeat": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "EditUserRequest": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "login": {
            "type": "string",
            "nullable": true
          },
          "password": {
            "type": "string",
            "nullable": true
          },
          "passwordRepeat": {
            "type": "string",
            "nullable": true
          },
          "email": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "RegisterRequest": {
        "type": "object",
        "properties": {
          "login": {
            "type": "string",
            "nullable": true
          },
          "password": {
            "type": "string",
            "nullable": true
          },
          "email": {
            "type": "string",
            "nullable": true
          },
          "passwordRepeat": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "ReplenishTheBalanceRequest": {
        "type": "object",
        "properties": {
          "userId": {
            "type": "string",
            "format": "uuid"
          },
          "quantityUsd": {
            "type": "number",
            "format": "double"
          }
        },
        "additionalProperties": false
      },
      "RestorePasswordRequest": {
        "type": "object",
        "properties": {
          "newPassword": {
            "type": "string",
            "nullable": true
          },
          "userId": {
            "type": "string",
            "format": "uuid"
          }
        },
        "additionalProperties": false
      },
      "SellCryptoRequest": {
        "type": "object",
        "properties": {
          "userId": {
            "type": "string",
            "format": "uuid"
          },
          "coinName": {
            "type": "string",
            "nullable": true
          },
          "quantityForSell": {
            "type": "number",
            "format": "double"
          }
        },
        "additionalProperties": false
      },
      "SendCryptoRequest": {
        "type": "object",
        "properties": {
          "receiverId": {
            "type": "string",
            "format": "uuid"
          },
          "senderId": {
            "type": "string",
            "format": "uuid"
          },
          "coinName": {
            "type": "string",
            "nullable": true
          },
          "quantityForSend": {
            "type": "number",
            "format": "double"
          }
        },
        "additionalProperties": false
      },
      "SendMessageRequest": {
        "type": "object",
        "properties": {
          "userId": {
            "type": "string",
            "format": "uuid"
          },
          "message": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "SendVerificationCodeRequest": {
        "type": "object",
        "properties": {
          "id": {
            "type": "string",
            "format": "uuid"
          }
        },
        "additionalProperties": false
      },
      "VerifyEmailRequest": {
        "type": "object",
        "properties": {
          "id": {
            "type": "string",
            "format": "uuid"
          },
          "code": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "WithdrawRequest": {
        "type": "object",
        "properties": {
          "userId": {
            "type": "string",
            "format": "uuid"
          },
          "quantityForWithdraw": {
            "type": "number",
            "format": "double"
          }
        },
        "additionalProperties": false
      }
    },
    "securitySchemes": {
      "Bearer": {
        "type": "apiKey",
        "description": "Please insert token",
        "name": "Authorization",
        "in": "header"
      }
    }
  },
  "security": [
    {
      "Bearer": [ ]
    }
  ]
}
```

Документация доступна по ссылке https://localhost:7157/swagger/v1/swagger.json

## Оценка качества кода 

![Описание изображения](https://github.com/NVERKHOVODKO/crypto-wallet/blob/main/Docs/staff/%D0%BC%D0%B5%D1%82%D1%80%D0%B8%D0%BA%D0%B8.jpg)
