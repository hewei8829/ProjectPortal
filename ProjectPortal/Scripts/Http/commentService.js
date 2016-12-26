//var app = angular.module('CommentApp', [])
//    .factory('commentFactory', function () {
//        //var factory = {};

//        //factory.getCommentSection = function (commentSectionId) {
//        //    var result = ApiCall.GetApiCall("http://projectportalservice.azurewebsites.net/api/values", commentSectionId)
//        //        .success(function (data) {
//        //            var data = angular.fromJson(data);
//        //            return data;
//        //        });
//        //};
//        //return factory;
//    })
//    .controller('CommentCtrl', function ($scope, commentFactory) {
//        $scope.testData = 'TestData';
//        //$scope.data = commentFactory.getCommentSection('5');
//    });




var appModule = angular.module('homeIndexPageApp', [])
.factory('commentFactory', function (ApiCall) {
    var factory = {};

    factory.GetData = function()
    {
        //var defer = $q.defer();
        var result = ApiCall.GetApiCall("http://projectportalservice.azurewebsites.net/api/values", "").success(function (data) {
            var data = angular.fromJson(data);
            result = (data);
            //temp = data;
            //defer.resolve(data);
            //data;
        });
        return result;
    }

    return factory;
})
.controller('CommentCtrl', function ($scope, commentFactory) {
    $scope.testData = 'TestData';



    $scope.testDataValue = commentFactory.GetData().success(function (data) {

        $scope.testDataValue = data;
        $scope.commentSection = data;

    });

    //$scope.testDataValue = data;


});

appModule.service('ApiCall', ['$http', function ($http) {
    var result;

    var config = { headers: { 'Content-Type': 'text/json' } };
    // This is used for calling get methods from web api
    this.GetApiCall = function (url, queryString) {
        result = $http.get(url + queryString, config).success(function (data, status) {
            result = (data);
        }).error(function () {
            alert("Something went wrong");
        });
        return result;
    };

    // This is used for calling post methods from web api with passing some data to the web api controller
    this.PostApiCall = function (URL, obj) {
        result = $http.post(URL, obj, config).success(function (data, status) {
            result = (data);
        }).error(function () {
            alert("Something went wrong");
        });
        return result;
    };
}]);



