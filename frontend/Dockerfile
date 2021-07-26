FROM node:15.14.0-alpine as build

WORKDIR /app

ENV PATH /app/node_modules/.bin:$PATH
COPY ./vontobel-ui/package.json ./
COPY ./vontobel-ui/package-lock.json ./
RUN npm install
RUN npm install react-scripts -g

ENV REACT_APP_API_URL http://localhost:1234

COPY ./vontobel-ui ./
RUN npm run build

FROM nginx:stable-alpine

COPY --from=build /app/build /usr/share/nginx/html
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]