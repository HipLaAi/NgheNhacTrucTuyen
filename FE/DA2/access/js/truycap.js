var app = angular.module('NgheNhacTrucTuyen', []);
app.controller("ControllCtrl", function ($scope, $http) {
     //---------Initialization-------------------------
 
     //-----------Function---------------------------------
     $scope.UploadFileIMG = function(file) {
         var formData = new FormData();
         formData.append('file', file);
 
         $http({
             method: "POST",
             url: current_url + '/api-admin/Tool/upload_IMG-tool',
             data: formData,
             headers: {
                 'Content-Type': undefined
             }
         })
         .then(
             function(response) {
                 var data = response.data;
                 if (data != null && data.message != null && data.message != 'undefined') {
                     alert(data.message);
                 } else {
                     $scope.img = data.fullPath;
                 }
             }
         );
     }
 
     $scope.ResetData = function(){
         $scope.img = null;
         $scope.audio = null;
         $scope.lyrics = null;
     }
 
     $scope.Login = function(){
        var item = {};
        item.username = document.getElementById('txtEmail').value;
        item.password = document.getElementById('txtPassWord').value;

        $http({
            method: 'POST',
            url: current_url + '/api-admin/TaiKhoan/login-account',
            data: item,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(
            function(response) {
                var data = response.data;
                if (data != null && data.message != null && data.message != 'undefined') {
                    alert(data.message);
                }
                else{
                    localStorage.setItem('Token', data.token);
                    if(data.loaitaikhoan == 1){
                        window.location.href = "quanlynhac.html";
                    }
                    else if(data.loaitaikhoan == 2){
                        window.location.href = "index.html";
                    }
                }
            },
            function() {
                checkUsser();
            }
        );
     }
     //---------------------Load-----------------
})
