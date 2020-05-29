$(document).ready(function () {
    $("#login").click(function () {
        let memberId = $("#memberId").val();
        if (memberId.trim() === "") {
            alert("Please insert a member id!");
            return;
        }
        let password = $("#password").val();
        if (memberId.trim() === "") {
            alert("Please insert a password!");
            return;
        }
        $.ajax({
            type: 'POST',
            url: "/login/login",
            contentType: 'application/json',
            data: JSON.stringify({
                memberId: memberId,
                password: password
            }),
            success: function (result) {
                if (result.isSuccess) {
                    alert("Login succeded");
                } else {
                    alert(result.errorMessage);
                }
            }
        });
    });
});