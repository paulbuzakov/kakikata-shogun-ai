# Kakikata Shogun Bot 🏯⚔️

A Telegram bot that helps users improve their American English language skills. The bot uses OpenAI's GPT-4 model to provide natural, fluent corrections and explanations for English phrases.

## Features

- Natural language correction and improvement
- Grammar, vocabulary, and tone adjustments
- Context-aware suggestions (casual, professional, or formal)
- Detailed explanations of corrections when needed
- Welcoming interface with a unique samurai theme

## Tech Stack

- .NET 9.0
- Telegram.Bot SDK
- OpenAI API
- PostHog Analytics
- Sentry Error Tracking

## Prerequisites

- .NET 9.0 SDK
- Linux server for deployment
- Required API keys:
  - Telegram Bot Token
  - OpenAI API Token
  - PostHog API Keys
  - Sentry DSN

## Configuration

1. Create `appsettings.user.json` with your API keys:
```json
{
  "TelegramBot": {
    "Token": "your-telegram-token"
  },
  "OpenAI": {
    "Model": "gpt-4",
    "Token": "your-openai-token"
  },
  "Sentry": {
    "Dsn": "your-sentry-dsn"
  },
  "PostHog": {
    "ProjectApiKey": "your-project-key",
    "Host": "your-posthog-host",
    "PersonalApiKey": "your-personal-key"
  }
}
```

## Development

```bash
# Build the project
make build

# Run locally
make run
```

## Deployment

```bash
# Deploy to production
make deploy

# View logs
make logs

# Service management
make start
make stop
make restart
make status
```

## License

[Add your license here]

## Contributing

[Add contribution guidelines here]

