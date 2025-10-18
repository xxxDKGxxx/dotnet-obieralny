# ===========================
# Etap 1: Build Angulara
# ===========================
FROM node:20-alpine AS build

# Ustaw katalog roboczy
WORKDIR /app

# Kopiuj pliki zależności
COPY package*.json ./

# Zainstaluj zależności (bez devDependencies można dodać --omit=dev)
RUN npm ci

# Skopiuj resztę projektu
COPY . .

# Zbuduj aplikację produkcyjną
RUN npm run build -- --configuration production

# ===========================
# Etap 2: Serwowanie aplikacji
# ===========================
FROM nginx:stable-alpine

# Usuń domyślny plik konfiguracji Nginx
RUN rm -rf /usr/share/nginx/html/*

# Skopiuj zbudowaną aplikację z poprzedniego etapu
COPY --from=build /app/dist/frontend/browser /usr/share/nginx/html

# (opcjonalnie) Skopiuj własny plik konfiguracyjny nginx.conf
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Otwórz port 80
EXPOSE 80

# Uruchom nginx
CMD ["nginx", "-g", "daemon off;"]
