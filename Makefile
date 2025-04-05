APP_NAME=KakikataShogun.Bot
PROJECT_DIR=KakikataShogun.Bot
PUBLISH_DIR=/var/kakikatabot/
RUNTIME=linux-x64
SERVICE_NAME=kakikatabot
SERVICE_FILE=/etc/systemd/system/$(SERVICE_NAME).service

build:
	dotnet build

run:
	dotnet run --project $(PROJECT_DIR)

publish:
	dotnet publish $(PROJECT_DIR) -c Release -r $(RUNTIME) --self-contained true -o $(PUBLISH_DIR)

deploy: publish
	cp $(SERVICE_NAME).service $(SERVICE_FILE)
	systemctl daemon-reload
	systemctl enable $(SERVICE_NAME)
	systemctl restart $(SERVICE_NAME)

logs:
	journalctl -u $(SERVICE_NAME) -f

status:
	systemctl status $(SERVICE_NAME)

restart:
	systemctl restart $(SERVICE_NAME)

stop:
	systemctl stop $(SERVICE_NAME)

start:
	systemctl start $(SERVICE_NAME)
