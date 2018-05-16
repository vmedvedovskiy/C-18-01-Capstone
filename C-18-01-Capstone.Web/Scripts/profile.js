$(document).ready(function () {
  $.ajax(
  {
    url: "/user/getProfileData/" + $("#login").val(),
    success: function (data) {
      $("#profileData").html(data)
    }
  });
})