
# ASP.NET Core Dynamic Page Loading System (AKA Better Pages)

## Overview
The ASP.NET Core Dynamic Page Loading System is a framework designed to streamline the loading of web pages by dynamically loading content into a base page using partial views. This approach allows for faster loading times and reduced data usage, as only the necessary content is fetched from the server.

![ezgif com-animated-gif-maker](https://github.com/MattterSteege/BetterPages/assets/78482881/bc0201bc-ae0f-4f22-afd3-21e3e144770a)*

As you can see the left column does not change, nor flicker during the page load (even with the Font Awesome icons). The only thing that changes is the content on the right side of the screen.

*This is a personal project for which I developed this system, but this is just **a** use of the system.

## Features

- **Dynamic Page Loading**: Load only the necessary content of web pages, reducing data transfer and speeding up page load times.
- **Partial View Integration**: Utilize ASP.NET Core's partial view feature to modularize page content and maintain a centralized framework.
- **Custom JavaScript Handling**: Implement custom JavaScript handling to minify scripts before sending them to the client, optimizing performance.
- **Centralized Framework**: Maintain a consistent base page layout while allowing for flexible content updates.

### Installation
To install the ASP.NET Core Dynamic Page Loading System, follow these steps:

1. Clone the repository:

   ```bash
   git clone https://github.com/MattterSteege/BetterPages.git
   ```

2. Navigate to the project directory:

   ```bash
   cd BetterPages
   ```

3. Build the project:

   ```bash
   dotnet build
   ```

4. Run the project:

   ```bash
   dotnet run
   ```

5. Access the application in your web browser at `http://localhost:5000` or `https://localhost:5001`.
## Fast Usage

1. Define your base page layout in the `Views/Shared/_Layout.cshtml` file, the `<main id="main">@RenderBody()</main>` part is the part that makes everyhting work, so don't delete it. The size of that element is also the bounds of the pages you load.
2. Create Partial Views with the content you want I.E. `Views/Main/Test.cshtml` and follow this design pattern
3. In the controller, make sure you `return PartialView()` and add the `[BetterPages]` attribute.

---
If you want, you can turn `<script>` tags into `<script minimize>` tags to take adventage of the automatic JS minifier built into the system.

## Contributing

Contributions are welcome! If you'd like to contribute to the ASP.NET Core Dynamic Page Loading System, please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes and commit them with descriptive commit messages.
4. Push your changes to your fork.
5. Submit a pull request to the main repository.

## License

[MIT](https://choosealicense.com/licenses/mit/)

