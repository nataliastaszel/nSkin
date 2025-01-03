# **DESCRIPTION**

An online shop with skincare products, a ASP NET CORE MVC application, version net8.0.
The UI is based on bootstrap watch theme "Litera" and bootstrap classes.

# **DEPENDENCIES**

- Microsoft.AspNetCore.Identity.EntityFrameworkCore 8.0.11
- Microsoft.AspNetCore.Identity.UI 8.0.10;8.0.11
- Microsoft.AspNetCore.EntityFrameworkCore 8.0.11
- Microsoft.AspNetCore.EntityFrameworkCore.Relational 8.0.11
- Microsoft.AspNetCore.EntityFrameworkCore.Sqlite 8.0.11
- Microsoft.AspNetCore.EntityFrameworkCore.SqlServer 8.0.11
- Microsoft.AspNetCore.EntityFrameworkCore.Tools 8.0.11
- Microsoft.VisualStudio.Web.CodeGeneration.Design 8.0.7

# **HOW TO RUN?**



# **DATABASE CONNECTION**

Connection to a database set in the appsettings.json.


### "DefaultConnection": 

"Server=localhost,1433; Database=nSkinShop; User=SA; Password=MyStrongPass123; TrustServerCertificate=True;"

# APPLICATION USERS

Application has 4 types of users. You can choose the type of your account during registration:

![UserTypes.png](ReadMeFiles/UserTypes.png)

### _Customer user_

email: applicationuser@gmail.com

password: Test098*

### _Admin user_

email: admin.test@gmail.com

password: Test098*


# **DESCRIPTION OF THE APPLICATION FROM THE USER'S POINT OF VIEW**

### 1. Home Functionalities

After launching the application, navigation is visible on the left side (Home, Privacy, Cart), and at the bottom, there is an option to log in or register. The main view displays products available for sale.
 
A user without logging in can read about the privacy policy and view product details. However, accessing the cart or adding items to the cart requires logging in.

![HomePage.png](ReadMeFiles/HomePage.png)
![ProductDetailsPage.png](ReadMeFiles/ProductDetailsPage.png)
![PrivacyPage.png](ReadMeFiles/PrivacyPage.png)
![LoginPage.png](ReadMeFiles/LoginPage.png)
![RegisterPage.png](ReadMeFiles/RegisterPage.png)


### 2. Admin Functionalities

After logging in, the admin user, like any other user, has access to their profile, the ability to manage content on the site, including categories, products, promotions, and companies, as well as access to the cart view and the ability to place orders.


![AdminHomeView.png](ReadMeFiles/AdminHomeView.png)

### Company CRUD

![ComapnyList.png](ReadMeFiles/ComapnyList.png)
![CompanyCreate.png](ReadMeFiles/CompanyCreate.png)
![CompanyEdit.png](ReadMeFiles/CompanyEdit.png)
![CompanyDelete.png](ReadMeFiles/CompanyDelete.png)

For the categories,promotions and product content management views are similar, the only difference is the product update view because of the image.

![ProductUpdate.png](ReadMeFiles/ProductUpdate.png)

### 3. Customer Functionalities

The customer user, like the admin, has access to their profile and all the functionalities within it, as well as the ability to place orders. However, 
they do not have permission to manage the store’s content.

In the cart view user can increase or decrease the amount of products as well as delete them. 
After that he can go to the summary of the order when he can see shipping details and place the order if the information is correct.


![CartPage.png](ReadMeFiles/CartPage.png)
![OrderSummaryPage.png](ReadMeFiles/OrderSummaryPage.png)
![OrderConfirmationPage.png](ReadMeFiles/OrderConfirmationPage.png)

### User Profile View

![UserProfilePage.png](ReadMeFiles/UserProfilePage.png)
![UserProfileEmailPage.png](ReadMeFiles/UserProfileEmailPage.png)
![UserProfilePasswordPage.png](ReadMeFiles/UserProfilePasswordPage.png)
![TwoFactorAuthenticationPage.png](ReadMeFiles/TwoFactorAuthenticationPage.png)
![UserPersonalDataPage.png](ReadMeFiles/UserPersonalDataPage.png)