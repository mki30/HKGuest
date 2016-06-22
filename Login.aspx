<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <meta charset="utf-8" />
    <link rel="shortcut icon" href="/images/favicon.ico" type="image/x-icon" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="google-signin-client_id" content="207688555644-obgftkaskb47ohr211c0u9ta065fcu9k.apps.googleusercontent.com" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script>
        var DoSignOut = false;
        var Impersonate = "";
        function onSignIn(googleUser)
        {   
            // The ID token you need to pass to your backend:
            var id_token = googleUser.getAuthResponse().id_token;

            if (DoSignOut)
                signOut();
            else
            {
                $.ajax({
                    type: "POST",
                    cache: false,
                    url: "data.aspx?Action=SignIn&Data1=" + Impersonate,
                    data: { token: id_token },
                    success: function (data)
                    {
                        if (data.Error == "OK" || data.Error == "")
                        {
                            console.log(data.Data);
                            if (data.Data == "OK")
                                location.href = "/";
                            else
                                alert(data.Data);
                        }
                        else
                            alert(data.Error);

                    }, error: function () { }
                });
            }
        }
        function signOut()
        {
            var auth2 = gapi.auth2.getAuthInstance();
            auth2.signOut().then(function ()
            {
                if (location.href != "/Login.aspx")
                    location.href = "/Login.aspx";
            });
        }
      
        function onFailure(error)
        {
            console.log(error);
        }
        function renderButton()
        {   
            gapi.signin2.render('g-signin2', {
                'scope': 'profile email',
                'width': 240,
                'height': 50,
                'theme': 'dark',
                'onsuccess': onSignIn,
                'onfailure': onFailure
            });
        }
    </script>
      <script src="https://apis.google.com/js/platform.js?onload=renderButton" ></script> 
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 200px; margin: auto; padding: 50px;">
            <div id="g-signin2"></div>
        </div>
    </form>
</body>
</html>
