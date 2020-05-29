$(document).ready(function () {
    $("#generatePassword").click(function () {
        let memberId = $("#memberId").val();
        if (memberId.trim() === "") {
            alert("Please insert a member id!");
            return;
        }
        $.ajax({
            type: 'POST',
            url: "/generatePassword/GenerateOneTimePassword",
            contentType: 'application/json',
            data: JSON.stringify({
                memberId: memberId
            }),
            success: function (result) {
                $("#password").html(result.value);
                showPasswordInformation(result.expirationDate);
            }
        });
    });

    function showPasswordInformation(expirationDate) {
        $("#timeContent").removeClass("hidden");
        $("#passwordContent").removeClass("hidden");
        $("#generatePassword").addClass("hidden");

        var countDownDate = new Date(expirationDate);
        countDownDate.setSeconds(countDownDate.getSeconds());

        var x = setInterval(function () {
            var now = new Date().getTime();

            var distance = countDownDate - now;
            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((distance % (1000 * 60)) / 1000);

            //document.getElementById("demo").innerHTML = minutes + "m " + seconds + "s ";
            $("#expirationTime").html(minutes + "m " + seconds + "s ");

            // If the count down is finished, write some text
            if (distance < 0) {
                clearInterval(x);
                $("#timeContent").addClass("hidden");
                $("#passwordContent").addClass("hidden");
                $("#passwordExpired").removeClass("hidden");
                $("#generatePassword").removeClass("hidden");
            }
        }, 1000);
    }   
});

