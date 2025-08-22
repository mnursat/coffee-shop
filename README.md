# CoffeeShop

Добро пожаловать в проект Coffee Shop!

## Описание
Coffee Shop — это приложение для продажи кофе, построенное на ASP.NET Core (Backend) и современном фронтенде Angular (Frontend).

## Структура проекта
```
CoffeeShop/
├── Backend/
│   ├── CoffeeShop.sln
│   ├── requests/           # Файлы для тестирования API (например, .http)
│   └── src/                # Исходный код Backend
└── Frontend/               # Исходный код Frontend
```

## Быстрый старт

### Backend
1. Перейдите в папку Backend:
   ```powershell
   cd Backend/src
   ```
2. Запустите проект:
   ```powershell
   dotnet run
   ```
3. Откройте Swagger по адресу, указанному в launchSettings.json (обычно https://localhost:7018/swagger).

### Frontend
Фронтенд реализован на Angular.

1. Перейдите в папку Frontend:
  ```powershell
  cd Frontend
  ```
2. Установите зависимости:
  ```powershell
  npm install
  ```
3. Запустите Angular dev server:
  ```powershell
  npm start
  ```
4. Откройте приложение по адресу:
  http://localhost:4200

## Работа с requests
В папке `requests` находятся файлы для тестирования API (например, `HealthCheck.http`, `CreateCoffee.http`). Их можно использовать с расширением REST Client для VS Code.

## Сборка и деплой
- Для сборки используйте:
  ```powershell
  dotnet build
  ```
- Для запуска через Docker:
  ```powershell
  docker compose up --build
  ```

## Примечания
- Для работы с requests-файлами рекомендуется расширение [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) для VS Code.

---
Если у вас есть вопросы или предложения — не стесняйтесь обращаться!

## Лицензия
Проект распространяется под лицензией MIT. См. файл LICENSE для подробностей.
