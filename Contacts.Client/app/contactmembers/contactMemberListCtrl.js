(function () {
    "use strict";
    angular
        .module("contact")
        .controller("contactMemberListCtrl", ["$scope", "$odataresource", "$timeout", "$route", contactMemberListCtrl]);
    function contactMemberListCtrl($scope, $odataresource, $timeout, $route) {

        /*Declare variables*/
        var contactDetail = this;
        $scope.contactModel = null;
        var serverPath = 'http://localhost:54521/api/';
        
        /*Get default Contact Details*/
        contactDetail.filterContacts = function () {

            var Contact = $odataresource(serverPath + 'contacts/:firstName');
            var contacts = null;

            if ($scope.contactModel) {
                contacts = Contact.odata()
                    .filter("FirstName", $scope.contactModel)
                    .query();
            }
            else {
                contacts = Contact.odata()
                    .query();
            }

            contactDetail.contactList = contacts;
        }

        /*Fetch First Name*/
        var getContacts = $odataresource(serverPath + 'contacts');
        var contacts = getContacts.odata()
            .query();
        contactDetail.contactDefaultList = contacts;
        /**/
        
        contactDetail.deleteContact = function (contact)
        {
            var deleteContact = $odataresource(serverPath + 'contacts/:id', { id: contact.contactID });
            var contact = new deleteContact();
            contact.$delete();
            $route.reload();
        }

        /*Intialize functions to call*/
        contactDetail.filterContacts();
    }
}());