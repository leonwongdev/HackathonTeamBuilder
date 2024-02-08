# Passion Project: Hackathon Team Builder

## Overveiw of the Files Added / Changed

- /Controllers
	- /AccountController.cs
		- Register(RegisterViewModel model): Added new fields to User model
	- /ManageController.cs
		- Index(ManageMessageId? message) : Added new User field to the view model so that user can read their profile info on `GET /Manage`
		- EditProfile(string Id): Added this function to display edit user form
		- EditProfile(EditUserProfileViewModel NewUserModel): Added this function to update user data in DB.
	- /HomeController.cs : 
		- Index(): For displaying list of hackathons
	- /HackathonDataController.cs
		- ListAll(): For getting list of hackathons from DB.
- /Models
	- AccountViewModels.cs
		- RegisterViewModel: Added new fields
	- ApplicationUserTeam.cs: Juncation class for Many to Many relationship of Team and User
	- Constant.cs: For holding constant value such as HTTPClient base address
	- EditUserProfileViewModel.cs : For rendering Edit form and update user record in DB
	- Hackathon.cs: Represent Hackathons table in DB
	- Helper.cs: Includes helper functions such as data for the drop down list of Hackathon role
	- IdentityModels.cs
		- ApplicationUser class: Added new fields
		- ApplicationDbContext class: Regiser new models
	- ManageViewModel.cs
		- IndexViewModel: Added Application user field so that user profile can be rendered on `GET /Manage`
	- Team.cs: Represent Teams table in the database.
- /Views
	- /Account
		- Register.cshtml: For user creation, added new input fields for user full name, bio, urls, and role.
	- /Manage
		- Index.cshtml: Added card layout to display user profile
		- EditProfile.cshtml: Added form to update user profile
	- /Shared
		- Error.cshtml: Added p tag to display error message.

# MVP Features

## User
### CRUD - Create

- MVC Endpoint: `GET /Account/Register` for displaying registration form
- MVC Endpoint: `POST /Account/Register` for creating user inside database
- WebAPI Endpoint: N/A
- Added extra fields (Full name, bio, linkedin url, github url, portfolio url) to user table
- When sign-up, user has to fill in all the information including the new fields added, the data will be stored in the database.
- Since user create function is built on top of .Net identity module, there is no WebAPI layer. User creation is done inside the `ApplicationController`
  and the use of `ApplicationUserManager` class, specifically the `UserManager.CreateAsync()` method.


### CRUD - Read
- MVC Endpoint: `GET /Manage` for displaying user profile
- WebAPI Endpoint: N/A (`ApplicationUserManager` class is used to communicate with database)
- Retrieving user data using the ManageController.Index() method, specifically with the `UserManager.FindByIdAsync(User.Identity.GetUserId())`
- The data is then added to the IndexViewModel and passed to the /Manage/Index.cshtml view
- Html code is added to /Manage/Index.cshtml to display the user information

### CRUD - Update
- MVC Endpoint: `GET /Manage/EditProfile` for displaying the edit form
- MVC Endpoint `POST /Manage/EditProfile` for updating record in the database
- WebAPI Endpoint: N/A (`ApplicationUserManager` class is used to communicate
- Update feature is implemented inside `ManageController.EditProfile(string Id)` and `ManageController.EditProfile(EditUserProfileViewModel NewUserModel)`
 with database)
