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

portainer:
	@echo "=====Starting portainer======"
	docker volume create portainer_data
	docker run -d -p 8000:8000 -p 9000:9000 -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer

test:
	go test -v -cover ./...\

production:
	go build -ldflags "-X main.version=production" cmd/api/main.go

lint:
	./golangci-lint run ./...
