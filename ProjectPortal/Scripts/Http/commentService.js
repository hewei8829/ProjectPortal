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

    factory.GetCommentSection = function (sectionId) {
        var result = ApiCall.GetApiCall("http://weiheblogservice.azurewebsites.net/api/comments/", sectionId + '?returnTimeSpan=true').success(function (data) {
            result = (data);
        });
        return result;
    }

    factory.AddComment = function (sectionId, commentContent, userName, userEmail) {
        var request =
                    {
                        "SubBlogCommentList": null,
                        "BlogCommentID": "",
                        "BlogCommentName": null,
                        "UserName": userName,
                        "Email": userEmail,
                        "CommentContent": commentContent,
                        "CommentTime": "",
                        "HeadIcon": null
                    };
        var result = ApiCall.PostApiCall("http://weiheblogservice.azurewebsites.net/Api/AddComment?sectionId=" + sectionId, request).success(function (data) {
            result = (data);
        });
        return result;

    }

    return factory;
})
.controller('CommentCtrl', function ($scope, commentFactory) {

    $scope.init = function(){ 
        commentFactory.GetCommentSection("826073ec-0e32-4c40-b010-f8c0e417aa1b").success(function (data) {
            var commentSection = angular.fromJson(data);
            $scope.commentSection = commentSection;
            $scope.numberOfCommnets = commentSection.BlogCommentList.length;
            $scope.inputCommentContent = '';
        });
    };

    //How to get the id from html
    $scope.AddComment = function () {
        commentFactory.AddComment("826073ec-0e32-4c40-b010-f8c0e417aa1b", $scope.inputCommentContent, $scope.inputUserName, $scope.inputUserEmail).success(function (data) {
            alert('Successfully submitted');
            $scope.init();

        }).error(function () {
            alert('Error happens while submit comment!');
        });
    }
});

//appModule.service('ApiCall', ['$http', function ($http) {
//    var result;

//    var config = { headers: { 'Content-Type': 'text/json' } };
//    // This is used for calling get methods from web api
//    this.GetApiCall = function (url, queryString) {
//        result = $http.get(url + queryString, config).success(function (data, status) {
//            result = (data);
//        }).error(function () {
//            alert("Something went wrong");
//        });
//        return result;
//    };

//    // This is used for calling post methods from web api with passing some data to the web api controller
//    this.PostApiCall = function (URL, obj) {
//        result = $http.post(URL, obj, config).success(function (data, status) {
//            result = (data);
//        }).error(function () {
//            alert("Something went wrong");
//        });
//        return result;
//    };
//}]);



