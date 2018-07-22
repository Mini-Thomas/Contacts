(function () {
    "use strict";
    angular
        .module("common.services")
        .factory('contactResource', 
        ["$http",
         "appSettings",
            contactResource])

    function contactResource($http, appSettings) {
        var all;
        /*oData queries
         /api/contacts?$skip=1&$top=3
         /api/contacts?$filter=ContactName eq 'Incredibles'
         /api/contacts?$orderby=ContactName desc
        */
        var getData = function () {
            return $http.get(appSettings.serverPath + "/api/contacts")
            .then(function (response) {
                all = response.data;
                return all;
            }).catch(angular.noop)
        }
        return {
            getData: getData
        };
    };
}());
