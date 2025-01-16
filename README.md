# Employee App

## Link to deployed app
employee-app-eegsgzadcefrbfak.polandcentral-01.azurewebsites.net

## Objective

The Employee App is designed to manage cars and rentals in a car rental. It provides functionalities for managing rentals and cars information and other related tasks.

## Features

- **Rental Management**: Complete the rental return, view all rentals.
- **Vehicle Management**: Add vehicles, update vehicles, right notes about their condition.

## File Structure

```
employee-app/
├── Pages/
│   ├── Home.razor                                   # Dashboard
│   ├── Login.razor                                  # Login page
│   ├── Rentals.razor                                # Rentals page
│   ├── Returns.razor                                # Returns page
│   └── Vehicles.razor                               # Vehicles page
├── Services/
│   ├── ICarService.cs                               # Interface for car service
│   ├── CarService.cs                                # Implementation of car service
│   ├── IRentalService.cs                            # Interface for rental service
│   ├── RentalService.cs                             # Implementation of rental service
│   ├── ILoginService.cs                             # Interface for login service
│   ├── LoginService.cs                              # Implementation of login service
│   └── EmployeeAuthenticationStateProvider.cs       # Implementation of employee service
├── Dtos/
│   ├── CarDto.cs                                    # Data transfer object for car
│   ├── CarCreateDto.cs                              # Data transfer object for creating a car
│   ├── CarUpdateDto.cs                              # Data transfer object for updating a car
│   ├── ...
│   └── RentalDto.cs                                 # Data transfer object for rental
├── Models/
│   ├── LoginModel.cs                                # Login model
│   └── Employee.cs                                  # Employee model
├── wwwroot/
│   ├── css/                                         # CSS files
│   └── ...                                          # Images, javascript files, ...
├── Helpers/
│   ├── Constants.cs                                 # Constants used in code
│   └── Enums.cs                                     # Enums used
├── Layout/
│   ├── MainLayour.razor                             # Main layout of the app
│   └── NavMenu.razor                                # Navigational menu of the app
├── App.razor                                        # Main application component
├── Program.cs                                       # Program entry point
└── .gitignore                                       # Git ignore file
```

## Getting Started
To get started with the Car Rental API, follow these steps:

1. **Clone the repository**:
   ```sh
   git clone <repository-url>
   cd employee-app
   ```

2. **Configure the database connection**:
   Update the connection strings `Program.cs` as needed.

3. **Run the application**:
   ```sh
   dotnet run
   ```
