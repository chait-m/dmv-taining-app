# DMVApp - Blazor WebAssembly DMV Application

## Overview
DMVApp is a Blazor WebAssembly application for submitting and managing vehicle title applications. It features a multi-step workflow, local storage using IndexedDB, and a modern, user-friendly interface.

---

## Features

### 1. Multi-Step New Vehicle Title Application
- Step-by-step form for entering vehicle and owner information, uploading documents, and reviewing a summary before submission.
- Client-side validation for all required fields.
- File upload support for required documents.

### 2. Application Number Generation
- Each application is assigned a unique, sequential 6-digit application number starting from 100001.
- Application numbers are managed and persisted in the browser's IndexedDB.

### 3. Local Database (IndexedDB) Integration
- All application data is stored in the browser using IndexedDB via JavaScript interop.
- Applications persist across browser sessions until cleared by the user.

### 4. Application Confirmation Page
- After submission, users are redirected to a dedicated confirmation page displaying all submitted details and the application number.
- Includes a link to start a new application with a fresh form.

### 5. Applications List & Data Management
- Dedicated page to view all submitted applications in a table.
- "Clear Data" button (only visible when applications exist) to delete all application data from IndexedDB.
- Bootstrap-styled confirmation modal for data deletion, with a warning and a temporary alert when data is cleared.

### 6. Modern UI Enhancements
- Modern, visually appealing header and navigation using Bootstrap and custom styles.
- Home page features a welcome banner and a prominent link to start a new application.
- Navigation menu includes links to Home, New Vehicle Title, and All Applications.

---

## Technical Details

- **Framework:** Blazor WebAssembly (.NET 9)
- **UI:** Bootstrap 5, custom CSS
- **Local Storage:** IndexedDB via JavaScript interop (see `wwwroot/dmvAppDb.js`)
- **Component Structure:**
  - `NewVehicleTitle.razor` (multi-step form)
  - `ApplicationConfirmation.razor` (confirmation page)
  - `Applications.razor` (list and manage applications)
  - Step components for each form section
- **Model:** `VehicleTitleModel` with all required fields and `ApplicationNumber`

---

## How to Use

1. **Start a New Application:**
   - Click "Apply for New Vehicle Title" on the Home page or use the navigation menu.
   - Complete the multi-step form and submit.
2. **View Confirmation:**
   - After submission, review your application details and number on the confirmation page.
   - Click "Create New Application" to start over.
3. **View All Applications:**
   - Use the "All Applications" link in the navigation menu to see all stored applications.
4. **Clear Data:**
   - If applications exist, use the "Clear Data" button on the Applications page.
   - Confirm in the modal dialog. A red alert will briefly confirm data deletion.

---

## Developer Notes

- All code-behind logic is separated into `.razor.cs` files for maintainability.
- IndexedDB logic is in `wwwroot/dmvAppDb.js` and exposed via JS interop.
- Bootstrap JS is loaded in `wwwroot/index.html` for modal support.
- The application is fully client-side; all data is stored in the browser.

---

## Recent Changes

- Refactored code-behind to partial classes.
- Added IndexedDB storage and sequential application number logic.
- Added confirmation page after submission.
- Added All Applications page with data clearing and confirmation modal.
- Improved UI with modern styles and navigation.
- Removed unused pages and flags.
- Enhanced user experience for form reset and navigation.

---

## License
This project is for demonstration and educational purposes.
