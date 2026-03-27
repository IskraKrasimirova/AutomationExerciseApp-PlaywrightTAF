# AutomationExercise Test Automation Framework

![CI Pipeline](https://github.com/IskraKrasimirova/AutomationExerciseApp-PlaywrightTAF/actions/workflows/ci.yml/badge.svg)


[![Allure Report](https://img.shields.io/badge/Allure-Report-6f42c1?logo=allure&logoColor=white)](https://iskrakrasimirova.github.io/AutomationExerciseApp-PlaywrightTAF/)


A test automation framework built with C# for UI and API testing of the [AutomationExercise](https://automationexercise.com) e-commerce practice website.

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Language | C# (.NET) |
| UI Testing | Microsoft Playwright |
| API Testing | RestSharp |
| Test Framework | NUnit |
| Assertions | FluentAssertions (API), Playwright Assertions (UI) |
| Test Data | Bogus (Faker) |

---

## Project Structure
```
    AutomationAppPlaywrightTAF/
├── AutomationApp.Common/          # Shared configuration and utilities
│
├── AutomationApp.ApiTests/        # API test project (RestSharp)
│   ├── Helpers/                   # API helper classes
│   ├── Models/                    # Request/response models and factories
│   ├── Tests/                     # Test classes
│   └── Utilities/                 # Constants and shared utilities
│       
│
└── AutomationApp.UiTests/         # UI test project (Playwright)
├── Models/                    # Test data models and factories
├── Pages/                     # Page Object classes
├── TestData/                  # Static test files
├── Tests/                     # Test classes
└── Utilities/                 # Constants and shared utilities
```

---

## Application Under Test

[AutomationExercise](https://automationexercise.com) is a full-featured e-commerce practice website designed for automation engineers. It provides:

- User registration and authentication
- Product browsing, search, and filtering by category and brand
- Shopping cart management
- Checkout and payment flow
- Contact form
- REST API endpoints for programmatic testing

---

## Test Coverage

### UI Tests (Playwright)

Tests are based on the test cases defined on the AutomationExercise website, with additional scenarios added for broader coverage.

| Test Class | Description |
|-----------|-------------|
| `RegisterTests` | User registration flow |
| `LoginTests` | Login, logout, invalid credentials |
| `ContactUsTests` | Contact form submission with and without file upload |
| `ProductsTests` | View product details, verify product information |
| `CartTests` | Add products, verify quantities and prices, remove products |
| `OrderTests` | Full E2E checkout flows: register during checkout, register before checkout |
| `CategoryTests` | Navigate and verify category product pages |
| `BrandTests` | Navigate and verify brand product pages |

### API Tests (RestSharp)

| Test Class | Description |
|-----------|-------------|
| `UserAccountTests` | Create, retrieve, update, and delete user accounts |
| `LoginTests` | Login and logout via API |
| `ProductsTests` | Get all products, get product by ID |
| `BrandsTests` | Get all brands |
| `SearchProductTests` | Search products by name |

---

## Design Patterns

- **Page Object Model (POM)** — each page is represented by a dedicated class encapsulating locators and actions
- **Base Page / Base Test** — shared setup and teardown logic
- **Factory Pattern** — test data generation using Bogus
- **Reusable Components** — `NavBar` and `Sidebar` are shared across pages

---

## Configuration

The base URL and other settings are configured in `appsettings.json`:
```json
{
  "BaseUrl": "https://automationexercise.com"
}
```

---

## Running the Tests

### Prerequisites

- .NET SDK
- Playwright browsers installed:
```bash
pwsh bin/Debug/net8.0/playwright.ps1 install
```

### Run all tests
```bash
dotnet test
```

### Run by category
```bash
dotnet test --filter Category=Smoke
dotnet test --filter Category=E2E
dotnet test --filter Category=UserAccountApi
```

---

## Allure Test Report

This project includes full Allure reporting for both UI and API test runs.
The report is automatically generated and published on every pipeline execution.

🔗 Live Report: [Allure Report](https://iskrakrasimirova.github.io/AutomationExerciseApp-PlaywrightTAF/)

### How it works

The CI pipeline:

- Cleans previous Allure results
- Runs API and UI tests
- Merges results from both projects
- Generates the HTML report using Allure CLI
- Deploys it automatically to GitHub Pages


To generate and view the Allure report locally after running tests, use the following command:
```bash
allure serve merged-results
```

This starts a local server and opens the interactive report in your browser.

---

## Notes

- Ad blocking is configured via Playwright route interception to prevent ads from interfering with test execution
- Tests use `NetworkIdle` load state where necessary to ensure page stability before interactions
- All test data is randomly generated per test run using Bogus
