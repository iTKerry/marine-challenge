# marine-challenge

## Task

Some vessels are equipped with VRS (hardware), this VRS transmits:
* IMO Number
* Vessel name
* Date & Time
* Position

Each VRS are individually configured to transmit data at a given interval. This might be every 5th minute or every 4th hour.
We wish to keep track of this data, the task is to create an API that can receive and store this data.
We also need to be able to see the data and make changes to it through a website. The website needs to give a good overview of the data.

## Run 

### Prerequisites
* docker 

To run the solution open repository root folder and run:

```
docker-compose up
```
### Services
* DB will be available at `localhost:1433`
* API will be available at `http://localhost:8888` with swagger url `http://localhost:8888/swagger/index.html`
* APP will be available at `http://localhost:9999`
