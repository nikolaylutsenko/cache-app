FROM node:22.11 AS build

WORKDIR /app

COPY package.json package.json
COPY package-lock.json package-lock.json

RUN npm install

COPY . .

RUN npm run build

FROM nginx:alpine

# Environment variables (can be overridden at runtime)
ENV NODE_ENV=production
ENV services__cacheapp__client__http__0=http://host.docker.internal:3000
ENV services__cacheapp__client__https__0=https://host.docker.internal:3001

COPY --from=build /app/default.conf.template /etc/nginx/templates/default.conf.template
COPY --from=build /app/dist/cacheapp.client/browser /usr/share/nginx/html

# Expose the default nginx port
EXPOSE 80

#SET $cacheapp_target ${services__cacheapp__client__https__0:-http://localhost:3000};


CMD ["nginx", "-g", "daemon off;"]
