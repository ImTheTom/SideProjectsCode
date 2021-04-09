build:
	@echo "=============Building Local API============="
	docker build -f Dockerfile -t api .

up:
	@echo "=============Starting Api Locally============="
	docker-compose up -d

logs:
	@echo "=============Pinting Logs Now================"
	docker-compose logs -f

stop:
	@echo "============Stopping Api Locally============="
	docker-compose stop

down:
	@echo "=============Stopping API Locally============="
	docker-compose down

delete:
	@echo "=====Removing API containers and volumes======"
	docker-compose down -v --remove-orphans

test:
	go test -v -cover ./...