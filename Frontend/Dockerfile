FROM node:latest AS build
WORKDIR /eventmeup
COPY . .
RUN npm ci
RUN npm run build:prod

FROM nginx:latest
COPY --from=build /eventmeup/dist/frontend /usr/share/nginx/html
EXPOSE 80
