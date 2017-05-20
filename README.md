# Gettings Started
1. Download and install [Visual Studio Community 2017](https://www.visualstudio.com/free-developer-offers/)
2. Download and install [Visual Studio Github Extension](https://visualstudio.github.com/)
3. Clone this repo using the `Team Explorer` in `Visual Studio`

# About Firebase
This project uses the following Firebase C# libraries:
1. Realtime database - [Firebase Database](https://github.com/step-up-labs/firebase-database-dotnet)
2. Authentication(login/logout/register) - [Firebase Auth](https://github.com/step-up-labs/firebase-authentication-dotnet)
3. Storage(uploading photos etc...) - [Firebase Storage](https://github.com/step-up-labs/firebase-storage-dotnet)

# Architecture Overview
Each feature is represented as a `Manager` class which is a class that holds some data and provides methods for each use case in it's domain, for example: `UserManager` holds the current `User` and provides methods like `Login`, `Register`, `Logout`. It also extends the `Observable` abstract class and calls `OnPropertyChanged(string propName)` whenever the current user changes so we can later bind it to the UI.

The UI is implemented via XAML Views and Their corrosponding `*.xaml.cs` controller classes. the controller class is responsible for routing clicks to methods of the manager class, and the XAML View is responsible for binding to the data in the manager class.
for example: `MasterPage.xaml.cs` will call `UserManager.logout()` when we click the logout button. And will show the current user via XAML bindings.
