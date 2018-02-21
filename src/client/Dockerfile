### STAGE 1: Build ###
FROM node:carbon as builder
LABEL authors="Bedabrata Chatterjee"

# Install app dependencies
# A wildcard is used to ensure both package.json AND package-lock.json are copied
# where available (npm@5+)
COPY package*.json ./

RUN npm set progress=false && npm config set depth 0 && npm cache clean --force

## Storing node modules on a separate layer will prevent unnecessary npm installs at each build
RUN npm i && npm install @angular/cli@latest && mkdir /ng-app && cp -R ./node_modules ./ng-app

# Create app directory
WORKDIR /ng-app

# Bundle app source
COPY . .

## Build the angular app in production mode and store the artifacts in dist folder
RUN $(npm bin)/ng build --prod --build-optimizer

# To test the build comment the nginx lines and uncomment the following lines
# EXPOSE 4200
# CMD [ "npm" , "start"]

### STAGE 2: Host using Nginx ###
FROM nginx:latest
COPY --from=builder /ng-app/dist/ /usr/share/nginx/html
COPY ./nginx-custom.conf /etc/nginx/conf.d/default.conf
