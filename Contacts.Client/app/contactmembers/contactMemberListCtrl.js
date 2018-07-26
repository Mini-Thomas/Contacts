(function () {
    "use strict";
    angular
        .module("contact")
        .controller("contactMemberListCtrl", ["$scope", "$odataresource", "$timeout", "$route", contactMemberListCtrl]);
    function contactMemberListCtrl($scope, $odataresource, $timeout, $route) {

        /*Declare variables*/
        var contactDetail = this;
        var contact = null;
        $scope.IsHidden = true;
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

        /*Delete Contact*/
        contactDetail.deleteContact = function ()
        {
            var deleteContact = $odataresource(serverPath + 'contacts/:id', { id: contact.contactID });
            var contactToDelete = new deleteContact();
            contactToDelete.$delete();
            $route.reload();
        }

        /*Confirmation pop out for Delete*/
        $scope.confirmationDialog = function (contactDelete) {
            contact = contactDelete;
            $scope.confirmationDialogConfig = {
                title: "Delete Contact",
                message: "Are you sure you want to delete?",
                buttons: [{
                    label: "Delete",
                    action: "delete"
                }]
            };
            $scope.showDialog(false);
        };

        $scope.showDialog = function (flag) {
            $scope.IsHidden = flag;
        };
        
        /*Intialize functions to call*/
        contactDetail.filterContacts();
    }
}());