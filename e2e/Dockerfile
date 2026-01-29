FROM mcr.microsoft.com/playwright:v1.45.0-jammy

WORKDIR /app

COPY package*.json ./
COPY playwright.config.ts ./

RUN npm ci

RUN npx playwright install chromium --with-deps

COPY . .

ENV BASE_URL="http://localhost:8080"

CMD ["npx", "playwright", "test"]
