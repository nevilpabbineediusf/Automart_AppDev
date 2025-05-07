API End Points Used:

  •	Image Upload API – Imgbb : https://api.imgbb.com/1/upload
    Purpose : Used to upload car listing images. When a user submits an image via the form, it’s sent to Imgbb, which returns a URL. That URL is stored in the database and used for display on the site.
    
  •	Location API – Map Api: https://nominatim.openstreetmap.org/search
    Purpose : Converts user-entered location names into geographic coordinates (latitude and longitude). This is used to enhance listings with geolocation-based features.
    
Updated ERD Diagram
[ERD Diagram](https://github.com/user-attachments/assets/a589878d-49e5-45b4-941f-e1057bb130c2)

CRUD Functionality Overview : 
    
   The AutoMart platform empowers registered Dealers to perform full CRUD (Create, Read, Update, Delete) operations on new and used car listings. These operations are built using ASP.NET Core MVC and Entity Framework Core, with secure authentication and role-based access control ensuring that only dealers can manage inventory.

  Create : Add a Car Listing
    •	Dealers can create new or used car listings through structured, role-specific web forms.
    •	Listings include attributes such as: Make, Model, Year, Price, Mileage (for used cars), Description, Location, and an uploaded Image.
    •	Images are uploaded asynchronously using the Imgbb API, and the resulting URL is stored in the database for display.
    •	Data integrity is ensured through server-side model validation and client-side input checks.
    
  Read : View Listings
    •	All listings (from all dealers) are publicly displayed on the Home Page using Razor Views and dynamic data binding.
    •	Listings are filterable by type (New or Used), and searchable by keywords (e.g., make, model, location).
    •	Each listing includes embedded images and mapped location data using OpenStreetMap.
    
  Update : Edit Existing Listings
    •	Only authenticated Dealers can update their own listings.
    •	Listings are retrieved via EF Core and displayed in pre-populated edit forms.
    •	Dealers can update car details and re-upload images, which are processed again via the Imgbb API.
    •	Both frontend and backend validation are used to ensure correct data entry.
    
   Delete : Remove Listings
    •	Dealers can delete their own listings with a single action from their dashboard.
    •	Deletion operations remove the record from the database using EF Core's data context.
    •	The UI reflects changes immediately, and future enhancements may include soft-delete mechanisms and confirmation prompts for safety.
    
 Notable Technical Challenges & Solutions :
   1.  Configuring Custom Authentication with Prefixed User IDs (D, U)
        Challenge: ASP.NET Core Identity does not natively support custom identifiers with role-based prefixes (e.g., D1001 for Dealers, U2002 for Users).
       
        Solution: We implemented a custom authentication system that identifies the user role by parsing the ID prefix at login. This role information is stored in session and used to control routing and feature access throughout the application.
        
  3. Ensuring Data Consistency Between New and Used Car Tables
       Challenge: The application uses two separate tables for new and used cars, each with unique schema attributes (e.g., Mileage only for used cars).
     
  	 Solution: We maintained consistency by abstracting shared logic through controller inheritance and partial views. Entity Framework Core was used to enforce data validation and model relationships while keeping table structures clean and modular.
      
  5. Image Upload Handling via External API
       Challenge: Image files needed to be uploaded and stored efficiently without consuming server storage.
     
       Solution: We integrated the Imgbb API at the backend level, where image files were handled during form submission. Uploaded image URLs were saved directly to the database, allowing listings to render lightweight remote image links.
     
  4. Handling Cross-Role Navigation and Session Management
       Challenge: Seamlessly managing different views and permissions based on user roles (Dealer vs. User) required precise control over navigation and session handling.
     
       Solution: Upon successful login, user details including role and ID were stored in the session. Controllers and views used this session data to enforce role-based access, restrict CRUD permissions to dealers only, and present appropriate content dynamically.
     

