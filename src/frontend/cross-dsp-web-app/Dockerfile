FROM node:25-alpine AS build

WORKDIR /app


# For env vars to be picked they need to be provided as args in build time.
ARG VITE_API_BASE_URL
ENV VITE_API_BASE_URL=${VITE_API_BASE_URL}

RUN npm install -g pnpm@latest-10
COPY ./package.json ./
COPY ./pnpm-lock.yaml ./
RUN pnpm install
COPY . .

RUN pnpm run build

FROM nginx:latest AS production
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]