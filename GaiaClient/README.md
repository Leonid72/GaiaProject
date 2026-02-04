# GaiaClient

# Gaia Project - Angular Client

A modern Angular 19 application for performing mathematical operations with a clean, responsive UI and real-time operation management.

## 🚀 Features

- **Calculator Interface**: Perform various mathematical operations with a sleek UI
- **Operation Management**: Enable/disable operations dynamically
- **Real-time Feedback**: Toast notifications and loading states
- **Operation History**: View last 3 operations and monthly usage statistics
- **Responsive Design**: Purple-themed UI with centered card layouts
- **API Integration**: Full REST API integration with error handling

## 📋 Prerequisites

Before running this project, make sure you have:

- Node.js (version 18 or higher)
- npm (comes with Node.js)
- Angular CLI (`npm install -g @angular/cli`)

## 🛠️ Installation

1. **Clone the repository**

   ```bash
   git clone <repository-url>
   cd GaiaClient
   ```

2. **Install dependencies**

   ```bash
   npm install
   ```

3. **Configure API endpoint**

   Update the API base URL in the environment files:
   - Development: [`src/environments/environment.ts`](src/environments/environment.ts)
   - Production: [`src/environments/environment.development.ts`](src/environments/environment.development.ts)

## 🚀 Development Server

Start the development server:

```bash
npm start
# or
ng serve
```

Navigate to `http://localhost:4200/`. The application will automatically reload when you make changes to source files.

## 🏗️ Build

Build the project for production:

```bash
npm run build
# or
ng build
```

Build artifacts will be stored in the `dist/gaia-client` directory.

## 🧪 Testing

Run unit tests:

```bash
npm test
# or
ng test
```

Tests are executed via [Karma](https://karma-runner.github.io) test runner.

## 📁 Project Structure

```
src/
├── app/
│   ├── models/
│   │   └── operation.model.ts          # Data transfer objects
│   ├── pages/
│   │   ├── calculator/                 # Calculator component
│   │   └── manage-operations/          # Operations management
│   ├── services/
│   │   └── operation.service.ts        # API service layer
│   ├── app.component.*                 # Root component
│   ├── app.config.ts                   # Application configuration
│   └── app.routes.ts                   # Routing configuration
├── environments/                       # Environment configurations
└── styles.css                         # Global styles
```

## 🎯 Key Components

### Calculator Component

- **Location**: [`src/app/pages/calculator/calculator.component.ts`](src/app/pages/calculator/calculator.component.ts)
- **Features**: Operation selection, input validation, result display, history tracking
- **Styling**: [`src/app/pages/calculator/calculator.component.css`](src/app/pages/calculator/calculator.component.css)

### Manage Operations Component

- **Location**: [`src/app/pages/manage-operations/manage-operations/manage-operations.component.ts`](src/app/pages/manage-operations/manage-operations/manage-operations.component.ts)
- **Features**: Toggle operation status, real-time updates, toast notifications

### Operation Service

- **Location**: [`src/app/services/operation.service.ts`](src/app/services/operation.service.ts)
- **Purpose**: Handle all API communications using HttpClient
- **Methods**: Get operations, execute calculations, update operation status

## 🔧 Configuration

### Environment Configuration

The app uses two environment files:

- **Development**: [`environment.ts`](src/environments/environment.ts) - Points to `https://localhost:5001`
- **Production**: [`environment.development.ts`](src/environments/environment.development.ts) - Points to `https://api.gaia.com`

### Angular Configuration

- **TypeScript**: Strict mode enabled with ES2022 target
- **Module Resolution**: Bundler mode for modern Angular
- **Testing**: Jasmine + Karma setup

## 📊 Data Models

The application uses TypeScript interfaces defined in [`operation.model.ts`](src/app/models/operation.model.ts):

- `OperationDto` - Operation metadata
- `OperationExecuteRequestDto` - Calculation request
- `OperationExecuteResponseDto` - Calculation response
- `OperationHistoryItemDto` - Historical operation data

## 🎨 Styling

- **Global Styles**: [`src/styles.css`](src/styles.css)
- **Theme**: Purple color scheme (`#7c5cc4`)
- **Layout**: Flexbox-based centered card layouts
- **Responsive**: Mobile-friendly design
- **Toast Notifications**: ngx-toastr integration

## 🔀 Routing

The application has the following routes defined in [`app.routes.ts`](src/app/app.routes.ts):

- `/` - Redirects to calculator
- `/calculator` - Main calculator interface
- `/manage` - Operations management
- `**` - Wildcard redirects to calculator

## 📦 Dependencies

### Core Dependencies

- **Angular 19.2.0** - Framework
- **RxJS 7.8.0** - Reactive programming
- **ngx-toastr 19.1.0** - Toast notifications

### Development Dependencies

- **Angular CLI 19.2.10** - Build tools
- **TypeScript 5.7.2** - Language
- **Jasmine + Karma** - Testing framework

## 🚨 Error Handling

The application implements comprehensive error handling:

- **API Errors**: Displayed via toast notifications
- **Validation Errors**: Form validation with visual feedback
- **Network Errors**: Graceful degradation with error messages

## 🔧 VS Code Configuration

The project includes VS Code configuration:

- **Launch Configuration**: [`launch.json`](.vscode/launch.json) - Debug configurations for ng serve and ng test
- **Tasks**: [`tasks.json`](.vscode/tasks.json) - Build and test tasks
- **Extensions**: [`extensions.json`](.vscode/extensions.json) - Recommended Angular extension

## 📈 Development Workflow

1. **Start development server**: `npm start`
2. **Run tests**: `npm test`
3. **Build for production**: `npm run build`
4. **Debug**: Use VS Code debug configurations

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Run tests to ensure quality
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License.

## 🆘 Support

For support and questions:

- Check the [Angular documentation](https://angular.dev)
- Review component implementations in the `src/app` directory
- Check environment configurations for API connectivity issues

## 🔮 Future Enhancements

- [ ] Add more mathematical operations
- [ ] Implement user authentication
- [ ] Add operation templates
- [ ] Include data visualization charts
- [ ] Add export functionality for operation history
