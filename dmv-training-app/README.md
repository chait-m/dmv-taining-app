# DMV Training App

This is a Blazor application for managing new vehicle title applications. The app is built with .NET 9 and C# 13, and demonstrates a multi-step form process for submitting vehicle title information, owner details, and required documents.

## Latest Changes

- Fixed all build errors for .NET 9 and C# 13 compatibility.
- Updated Blazor component usage and binding syntax to use `@bind-Value` and correct parameter passing.
- Added required `@using` directives for Blazor components and JS interop in all relevant files.
- Updated JavaScript interop calls to use the correct overloads for .NET 9 (object array for arguments).
- Ensured all multi-step vehicle title application components (Step1VehicleInfo, Step2OwnerInfo, Step3DocumentUpload, Step4Summary) work together seamlessly.
- Addressed nullable reference warnings and improved code-behind accessibility for Razor markup.

## Features

- Multi-step form for new vehicle title applications
- Owner and vehicle information collection
- Document upload for required paperwork
- Application summary and confirmation
- Local storage integration using JavaScript interop

## Getting Started

1. Ensure you have .NET 9 SDK installed.
2. Build and run the project using your preferred IDE or `dotnet run`.
3. Navigate to `/new-vehicle-title` to start a new application.

## Project Structure

- `Pages/Components/` - Contains Blazor components for each step of the application process.
- `Models/VehicleTitleModel.cs` - Data model for vehicle title applications.
- `Pages/NewVehicleTitle.razor` - Main multi-step form page.
- `Pages/Applications.razor` - View all applications.
- `Pages/ApplicationConfirmation.razor` - Confirmation page after submission.

## Notes

- The app uses local storage for data persistence via JS interop.
- All code is updated for .NET 9 and Blazor best practices.

---

For any issues or suggestions, please open an issue or submit a pull request.
