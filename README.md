# Passion Project: Hackathon Team Builder

# Getting Started
1. Navigate to `/Models/Constant` and change the `BASE_URL` to match your localhost and port number.
2. Open Packag Manager Console, run `Update-Database` to execute the migrations.
3. Start the application

# Database Design

# Files Added / Changed

- /Controllers
	- /AccountController.cs
		- Register(RegisterViewModel model): Added new fields to User model
	- /ManageController.cs
		- Index(ManageMessageId? message) : Added new User field to the view model so that user can read their profile info on `GET /Manage`
		- EditProfile(string Id): Added this function to display edit user form
		- EditProfile(EditUserProfileViewModel NewUserModel): Added this function to update user data in DB.
	- /HomeController.cs : 
		- Index(): For displaying list of hackathons
	- /HackathonController.cs: For CRUD of hackathon table, refer to MVP feature section below.
	- /HackathonDataController.cs: For WebAPI layer of CRUD on the hackathon table.
- /Models
	- AccountViewModels.cs
		- RegisterViewModel: Added new fields
	- ApplicationUserTeam.cs: Junction class for Many to Many relationship of Team and User
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
	- /Hackthon: Contains views for CRUD operation on hackathon table.
	- /Shared
		- Error.cshtml: Added p tag to display error message.

# MVP Features

## AspNetUsers Table
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

### Delete
- MVC Endpoint: `POST /Manage/DeleteUser?UserId={UserId}`
- User will see a delete button in `/Manage/Index`, they can delete their own account, and once deleted successfully, they will be logged out.

## Hackathons Table

### Create
- MVC Endpoint: `GET /hackathon/create` for displaying a create form
- WebAPI Endpoint: `GET /api/hackathon/create` for creating a record in db.


### Read
- MVC Endpoint: `GET /home` for displaying a list of hackathon on home page
- WebAPI Endpoint: `GET /api/hackathon/listall` for getting a list of hackathon object
- WebAPI Endpoint `GET /api/hackthon/findbyid/{id}` for getting hackathon by id

### Update
- MVC Endpoint: `GET /hackathon/update` for displaying a update form with value from selected hackathon record.
- MVC Endpoint: `POST /hackathon/update` for sending request to web api to update record and then redirect to home page.
- WebAPI Endpoint: `GET /api/hackathon/update` for updating a record in DB.

### Delete
- MVC Endpoint: `GET /hackathon/deleteConfirm` for displaying confirmation page before proceed to deletion.
- MVC Endpoint: `POST /hackathon/delete` for sending request to web api for deletion and then redirect user to the home page.
- WebAPI Endpoint: `DELETE /api/hackathondata/delete` for deleting a record in DB.

## Teams table
#### Remark: Team Controller is annotated with `[Authorize]` attribute to ensure only logged in user can view teams and join a team.
### Create
- MVC Endpoint: `GET /Team/Create` for displaying the team creation form.
- MVC Endpoint: `POST /Team/Create` for sending form data to web api.
- WebAPI Endpoint: `POST /api/TeamData/CreateTeamWithLeader` for creating a team. The user creating the team will be the team leader. It will also update the ApplicationUserTeams table (A junction table for M-M relationship between User and Team)


### Read
- MVC Endpoint: `GET /Team/List/{id}` for displaying a list of team by a hackathon id.
- WebAPI Endpoint: `GET /api/teamdata/ListTeamsByHackathon/{Id}` for getting a list of team by hackathon id from database.

### Update
- MVC Endpoint: `GET /Team/Update` for displaying a form to update team requirements.
- MVC Endpoint: `POST /Team/Update` for sending new team data to web api layer.
- WebAPI Endpoint: `POST /api/Team/Update` for updating data in database layer.

### Delete
- MVC Endpoint: `GET /Team/DeleteConfirm/{id}` for displaying confirming page before deleting the team.
- MVC Endpoint: `GET /Team/Delete` for sending new team data to web api layer.
- WebAPI Endpoint: `DELETE /api/Team/Update` for updating data in database layer. It will first delete every row in ApplicationUserTeams table by team Id, then delete the team, to maintain referential integrity.

## ApplicationUserTeams Table
Description: This table is a junction table between the ApplicationUser class (AspNetUser table) and Team class (Teams table).

### CREATE (Joining a team)
- MVC Endpoint: `POST /TeamMember/JoinTeam`, triggers when users clicks the join button, the join button is inside a form with hidden fields that contains the current team id, hackathon id and current user id. These data will be used for creating a relationship in the table. 
- WebAPI Endpoint `POST /api/TeamMemberData/JoinTeam` insert an record to the ApplicationUserTeams table in the database.

### Read
- MVC Endpoint: `GET /TeamMember/List/{id}` for displaying all member in a team by team id.
- WebAPI Endpoint: `GET /api/teammemberdata/list/{id}` for getting a list of ApplicationUserTeam object from the database. It means getting a list of team members by team id.

### Update
- Update function is not implemented as I do not want the user have the ability to switch teams, they should either join a team or quit a team.

### Delete (Quitting a team)
- MVC Endpoint: `POST /TeamMember/QuitTeam`, triggers when user clicks the Quit Team button.
- WebAPI Endpoint: `POST /api/TeamMemberData/QuitTeam` for deleting a record in the table using the hackathon id, team id, and user id in the ApplicationUserTeam object.


# Possible Improvements
- Authorisation feature: Currently any user is able to perform CRUD on Hackathon table, it would be better to add a role-based authorisation feature to only allow admin to manage hackathons.
- Team member removal: Ability for team lead to remove members. Currently only members can quit or join a team, and team lead does not have ability to remove a team member.