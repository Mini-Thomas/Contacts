(function () {
    "use strict";
    angular
        .module("contact")
        .controller("add_editContactCtrl", ["$scope", "$route", "$odataresource", "$routeParams", "$window", add_editContactCtrl]);
    function add_editContactCtrl($scope, $route, $odataresource, $routeParams, $window) {

        /*Declare Variables*/
        var add_editContact = this;
        var serverPath = 'http://localhost:54521/api/';
        $scope.index = $routeParams.contactID;
        $scope.todo = null;
        var model = $scope.model;
        add_editContact.heading = "Add Contact";
        
        /*function to submit the form after all validation has occurred*/            
        add_editContact.submitForm = function (isValid) {
            // check to make sure the form is completely valid
            if (isValid) {
                add_editContact.saveContactDetails();
            }
        };
        
        /*Save Contact Details*/
        add_editContact.saveContactDetails = function () {
            var jsModel = {};
            var saveContact = null;
            var contact = null;
            jsModel.firstName = add_editContact.contact[0].firstName;
            jsModel.lastName = add_editContact.contact[0].lastName;
            jsModel.email = add_editContact.contact[0].email;
            jsModel.phoneNumber = add_editContact.contact[0].phoneNumber;
            jsModel.status = add_editContact.contact[0].status;

            if (add_editContact.contact[0].contactID) {
                saveContact = $odataresource(serverPath + 'contacts/:id', { id: parseInt($scope.index) });
                contact = new saveContact(jsModel);
                contact.$update();
            }
            else {
                saveContact = $odataresource(serverPath + 'contacts');
                contact = new saveContact(jsModel);
                contact.$save();
            }
            $route.reload();
            $window.location.href = 'Index.html#!/contacts';
        }

         /*Fetch Contact Details as per ContactID for Edit Call*/
        add_editContact.getContact = function () {
            var Contact = $odataresource(serverPath + 'contacts/:id', { id: parseInt($scope.index)});
            var contacts = Contact.odata()
                .query();
            add_editContact.contact = contacts;
         }
       
        if ($scope.index) {
            add_editContact.heading = "Edit Contact";
            add_editContact.getContact();
        }
    }//main class
}());