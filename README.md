# AutomationApp Test Framework

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
├── AutomationApp.Common/
│   ├── Models/
│   │   └── SettingsModel.cs
│   └── Utilities/
│       └── ConfigurationSettings.cs
│
├── AutomationApp.ApiTests/
│   ├── Helpers/
│   │   └── UserApiHelper.cs
│   ├── Models/
│   │   ├── Brands/
│   │   ├── Products/
│   │   └── Users/
│   │       ├── Factories/
│   │       ├── UserDetailModel.cs
│   │       ├── UserDetailResponse.cs
│   │       └── UserModel.cs
│   │   └── ApiResponse.cs
│   ├── Tests/
│   │   ├── BaseTest.cs
│   │   ├── BrandsTests.cs
│   │   ├── LoginTests.cs
│   │   ├── ProductsTests.cs
│   │   ├── SearchProductTests.cs
│   │   └── UserAccountTests.cs
│   └── Utilities/
│       └── ApiConstants.cs
│
└── AutomationApp.UiTests/
    ├── Models/
    │   ├── Factories/
    │   │   ├── ContactFormFactory.cs
    │   │   ├── PaymentFactory.cs
    │   │   └── UserFactory.cs
    │   ├── ContactFormModel.cs
    │   ├── PaymentModel.cs
    │   ├── ProductData.cs
    │   └── UserModel.cs
    ├── Pages/
    │   ├── AccountCreatedPage.cs
    │   ├── AccountDeletedPage.cs
    │   ├── BasePage.cs
    │   ├── BrandProductsPage.cs
    │   ├── CartModal.cs
    │   ├── CartPage.cs
    │   ├── CategoryProductsPage.cs
    │   ├── CheckoutModal.cs
    │   ├── CheckoutPage.cs
    │   ├── ContactUsPage.cs
    │   ├── HomePage.cs
    │   ├── LoginPage.cs
    │   ├── NavBar.cs
    │   ├── OrderConfirmationPage.cs
    │   ├── PaymentPage.cs
    │   ├── ProductDetailsPage.cs
    │   ├── ProductsPage.cs
    │   ├── Sidebar.cs
    │   └── SignupPage.cs
    ├── TestData/
    │   └── test-file.txt
    ├── Tests/
    │   ├── BaseTest.cs
    │   ├── BrandTests.cs
    │   ├── CartTests.cs
    │   ├── CategoryTests.cs
    │   ├── ContactUsTests.cs
    │   ├── LoginTests.cs
    │   ├── OrderTests.cs
    │   ├── ProductsTests.cs
    │   └── RegisterTests.cs
    └── appsettings.json
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

## Notes

- Ad blocking is configured via Playwright route interception to prevent ads from interfering with test execution
- Tests use `NetworkIdle` load state where necessary to ensure page stability before interactions
- All test data is randomly generated per test run using Bogus
