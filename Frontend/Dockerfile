FROM node:latest AS build
WORKDIR /squashmeup
COPY . .
RUN npm ci
RUN npm run build:prod

FROM nginx:latest
COPY --from=build /squashmeup/dist/frontend /usr/share/nginx/html
EXPOSE 80
