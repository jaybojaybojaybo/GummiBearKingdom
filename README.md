# Gummi Bear Kingdom
#### by Jasun Feddema

## Description
Your source for all things Gummi Bear!

C#/dotNET web experience.

##User Stories

* As a user, I want to click on a link on the Landing page that takes me to a page that lists all available Products.
* As a user, I want to be able to click on each Product and see its Details.
* As an admin user, I want to be able to add and remove individual Products, as well as delete all Products.
* As a user, I want to be able to add a review to a product.
* As a user, I want to see the average rating for a product on its Details page.


## Setup/Installation

* Make sure Visual Studio 2017 is installed on your machine.
* Clone repository from [here](https://github.com/jaybojaybojaybo/GummiBearKingdom)
* Open solution in Visual Studio and run a build on the project.
* Push the Play Button with 'IIS Express' option selected.
  * (If not in testing, comment out the top half of the Products Controller and comment in the bottom half)
* To run all Tests, press Ctrl+R then A.
  * (If testing, comment out the top half of the Products Controller and comment in the bottom half)

## Database Migration

* Make sure MAMP is setup on your machine.
* Open MAMP and start servers.
* In the command line, enter the following command: "dotnet ef database update"

- OR -

* import the localhost.sql fil into the MAMP server for seeded data

## Technologies Used

* C#
* ASP.NET
* Entity Framework
* Visual Studio 2017

## Support and contact details

* contact the author at jasun.feddema@gmail.com

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).

### License

Copyright (c) 2018 Jasun Feddema

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
