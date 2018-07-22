(function () {
    "use strict";
    //var app = angular.module("contact", ["common.services","ODataResources"]); //v1 changes
    var app = angular.module("contact", ["ODataResources", "ngRoute", "ngSanitize"]).
    config(['$routeProvider', function ($routeProvider) {
        $routeProvider.
              when("/addContact", { templateUrl: "/app/contactmembers/add_editContact.html", controller: "add_editContactCtrl" }).
              when("/editContact/:contactID", { templateUrl: "/app/contactmembers/add_editContact.html", controller: "add_editContactCtrl" }).
              when("/contacts", { templateUrl: "/app/contactmembers/contactMemberListView.html", controller: "contactMemberListCtrl" }).
              otherwise({ redirectTo: '/contacts' });
    }]);
}());