ifneq (,$(wildcard .env))
include .env
export
endif

.DEFAULT_GOAL := help
.PHONY: help init build up down clean ps logs psql

help:
	@echo "make init   -  create .env from .env.example"
	@echo "make build  -  build images"
	@echo "make up     -  container startup"
	@echo "make down   -  stop and delete container"
	@echo "make clean  -  stop and delete container with volumes"
	@echo "make ps     -  container status"
	@echo "make logs   -  show container logs"
	@echo "make psql   -  cmd for PostgreSQL"

init:
	@if [ -f .env ]; then \
		echo ".env already exists"; \
	else \
		cp .env.example .env; \
		CURRENT_UID=$$(id -u); \
		CURRENT_GID=$$(id -g); \
		sed -i.bak "s/^UID=.*/UID=$$CURRENT_UID/" .env; \
		sed -i.bak "s/^GID=.*/GID=$$CURRENT_GID/" .env; \
		rm -f .env.bak; \
		echo ".env created"; \
	fi

build:
	@docker compose build

up:
	@docker compose up -d

down:
	@docker compose down

clean:
	@docker compose down -v

ps:
	@docker compose ps

logs:
	@docker compose logs -f

psql:
	@docker compose exec postgres psql -U $(POSTGRES_USER) -d $(POSTGRES_DB)
