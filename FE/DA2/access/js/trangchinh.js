var app = angular.module('NgheNhacTrucTuyen', []);
app.controller("UserCtrl", function ($scope, $http) {
     //---------Initialization-------------------------
     $scope.listTopNewAlbum;
     //-----------Function---------------------------------
     $scope.TopNewAlbum = function(){
          $http({
               method: "POST",
               url: current_url + '/api-user/Album/topnew-album?top=' + 6,
               headers: {
                    'Content-Type': undefined
               }
          }).then(
               function(response){
                    $scope.listTopNewAlbum = response.data;
               }
          )
     }
     //---------------------Load-----------------
     $scope.TopNewAlbum()
})
