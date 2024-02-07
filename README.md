# Passion Project: Hackathon Team Builder

# MVP Features

## User
### CRUD - Create
- Added extra fields (Full name, bio, linkedin url, github url, portfolio url) to user table
- When sign-up, user has to fill in all the information including the new fields added, the data will be stored in the database.
- MVC Endpoint: `GET /Account/Register`
- WebAPI Endpoint: N/A
- Since user create function is built on top of .Net identity module, there is no WebAPI layer. User creation is done inside the `ApplicationController`
  and the use of `ApplicationUserManager` class, specifically the `UserManager.CreateAsync()` method.


### CRUD - Read
- Retrieving user data using the ManageController.Index() method, specifically with the `UserManager.FindByIdAsync(User.Identity.GetUserId())`
- The data is then added to the IndexViewModel and passed to the /Manage/Index.cshtml view
- Html code is added to /Manage/Index.cshtml to display the user information
- MVC Endpoint: `GET /Manage`
- WebAPI Endpoint: N/A (Because read feature is rely on the `ApplicationUserManager` class from the .Net Identity)
