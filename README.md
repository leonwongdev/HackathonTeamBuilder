# Passion Project: Hackathon Team Builder

# MVP Features

## User
### CRUD - Create
- Added extra fields (Full name, bio, linkedin url, github url, portfolio url) to user table
- When sign-up, user has to fill in all the information including the new fields added, the data will be stored in the database.
- MVC Endpoint: `GET /Account/Register` for displaying registration form
- MVC Endpoint: `POST /Account/Register` for creating user inside database
- WebAPI Endpoint: N/A
- Since user create function is built on top of .Net identity module, there is no WebAPI layer. User creation is done inside the `ApplicationController`
  and the use of `ApplicationUserManager` class, specifically the `UserManager.CreateAsync()` method.


### CRUD - Read
- Retrieving user data using the ManageController.Index() method, specifically with the `UserManager.FindByIdAsync(User.Identity.GetUserId())`
- The data is then added to the IndexViewModel and passed to the /Manage/Index.cshtml view
- Html code is added to /Manage/Index.cshtml to display the user information
- MVC Endpoint: `GET /Manage` for displaying user profile
- WebAPI Endpoint: N/A (`ApplicationUserManager` class is used to communicate with database)

### CRUD - Update
- Update feature is implemented inside `ManageController.EditProfile(string Id)` and `ManageController.EditProfile(EditUserProfileViewModel NewUserModel)`
- MVC Endpoint: `GET /Manage/EditProfile` for displaying the edit form
- MVC Endpoint `POST /Manage/EditProfile` for updating record in the database
- WebAPI Endpoint: N/A (`ApplicationUserManager` class is used to communicate with database)
