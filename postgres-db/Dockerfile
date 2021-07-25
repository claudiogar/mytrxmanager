# syntax=docker/dockerfile:1

FROM gangstead/postgis:13-3.1-arm
RUN mkdir "/initializators"
ADD "./01-init.sh" "/docker-entrypoint-initdb.d"
ADD "./02-migrations.sql" "/initializators"
RUN ["chmod", "+x", "/docker-entrypoint-initdb.d/01-init.sh"]
